using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PelatologioApp.Models;
using PelatologioApp.Data;

namespace PelatologioApp
{
    public partial class AppointmentForm : Form
    {
        // Αν περάσει υπάρχων πελάτης
        private readonly Customer? _existingCustomer;
        private readonly bool _isNewCustomer;

        // Επιλογές που διαβάζει το Form1
        public int SelectedServiceId { get; private set; }
        public string SelectedServiceName { get; private set; } = "";
        public DateTime SelectedStart { get; private set; }
        public DateTime SelectedEnd { get; private set; }

        // A ή B
        public string SelectedUserCode { get; private set; } = "A";

        // Στοιχεία πελάτη (για νέο)
        public string CustomerName =>
            _isNewCustomer
                ? txtName.Text.Trim()
                : _existingCustomer?.FullName ?? string.Empty;

        public string CustomerPhone =>
            _isNewCustomer
                ? txtPhone.Text.Trim()
                : _existingCustomer?.Phone ?? string.Empty;

        // Waitlist
        public bool WantsEarlier => chkWantsEarlier.Checked;

        public TimeSpan? PreferredBefore
        {
            get
            {
                if (!chkWantsEarlier.Checked) return null;

                var sel = cmbPreferredTime.SelectedItem?.ToString();
                if (sel == "Πριν τις 14:00") return new TimeSpan(14, 0, 0);
                if (sel == "Μετά τις 17:00") return new TimeSpan(17, 0, 0);
                if (sel == "Μετά τις 18:00") return new TimeSpan(18, 0, 0);
                return null;
            }
        }

        // Οι δικές σου εργασίες
        private readonly List<Service> _services = new()
        {
            new Service { Id = 1, Name = "Μανικιούρ", Duration = 60 },
            new Service { Id = 2, Name = "Πεντικιούρ", Duration = 60 },
            new Service { Id = 3, Name = "Ονυχοκρύπτωση", Duration = 60 },
            new Service { Id = 4, Name = "Κάλοι", Duration = 60 },
            new Service { Id = 5, Name = "Σκληρύνσεις", Duration = 60 },
            new Service { Id = 6, Name = "Οστρακοειδή νύχια", Duration = 60 },
            new Service { Id = 7, Name = "Τεχνική ορθονυχίας", Duration = 60 },
        };

        // Νέος πελάτης
        public AppointmentForm()
        {
            InitializeComponent();
            _isNewCustomer = true;
        }

        // Υπάρχων πελάτης
        public AppointmentForm(Customer existing)
        {
            InitializeComponent();
            _existingCustomer = existing;
            _isNewCustomer = false;
        }

        private void AppointmentForm_Load(object sender, EventArgs e)
        {
            // Εργασίες
            cmbService.DataSource = _services;
            cmbService.DisplayMember = "Name";
            cmbService.ValueMember = "Id";
            if (_services.Any())
                cmbService.SelectedIndex = 0;

            // Ημερομηνία
            dtDate.Value = DateTime.Today;
            dtDate.MinDate = DateTime.Today;

            // Χρήστης A/B
            cmbUser.Items.Clear();
            cmbUser.Items.Add("A");
            cmbUser.Items.Add("B");
            cmbUser.SelectedIndex = 0;

            // Αν υπάρχων πελάτης → εμφάνιση & κλείδωμα
            if (!_isNewCustomer && _existingCustomer != null)
            {
                txtName.Text = _existingCustomer.FullName;
                txtPhone.Text = _existingCustomer.Phone;
                txtName.ReadOnly = true;
                txtPhone.ReadOnly = true;
            }
            else
            {
                txtName.ReadOnly = false;
                txtPhone.ReadOnly = false;
                txtPhone.KeyPress += TxtPhone_KeyPress; // μόνο αριθμοί
            }

            // Waitlist preferred time
            cmbPreferredTime.Enabled = chkWantsEarlier.Checked;
            chkWantsEarlier.CheckedChanged += (s, ev) =>
            {
                cmbPreferredTime.Enabled = chkWantsEarlier.Checked;
            };
            if (cmbPreferredTime.Items.Count > 0)
                cmbPreferredTime.SelectedIndex = 0;

            // Slots
            LoadSlots();
            dtDate.ValueChanged += (s, ev) => LoadSlots();
            cmbService.SelectedIndexChanged += (s, ev) => LoadSlots();
            cmbUser.SelectedIndexChanged += (s, ev) => LoadSlots();

        }

        private void LoadSlots()
        {
            if (cmbService.SelectedItem is not Service svc)
                return;

            lstHours.Items.Clear();

            var date = dtDate.Value.Date;
            var start = date.AddHours(9);
            var end = date.AddHours(20);
            var dur = TimeSpan.FromMinutes(svc.Duration);

            // Ποιος χρήστης (A/B) κλείνει
            var userCode = (cmbUser.SelectedItem?.ToString() ?? "A");

            // Διαβάζουμε ΟΛΑ τα ραντεβού από το storage
            var allAppointments = FileStorage.LoadAppointments();

            while (start + dur <= end)
            {
                var slotStart = start;
                var slotEnd = start + dur;

                // Έλεγχος αν υπάρχει ήδη ραντεβού που χτυπάει σε αυτό το διάστημα
                // για τον ΣΥΓΚΕΚΡΙΜΕΝΟ χρήστη (A/B)
                bool taken = false;

                foreach (var a in allAppointments)
                {
                    if (a.Status != "booked") continue;
                    if (!string.Equals(a.UserCode, userCode, StringComparison.OrdinalIgnoreCase)) continue;

                    // Φιλτράρουμε μόνο τα ραντεβού της ίδιας ημέρας
                    if (a.Start.Date != date) continue;

                    // overlap check: υπάρχει σύγκρουση με αυτό το slot;
                    if (!(a.End <= slotStart || a.Start >= slotEnd))
                    {
                        taken = true;
                        break;
                    }
                }


                // Αν δεν είναι κατειλημμένο, το προσθέτουμε στη λίστα
                if (!taken)
                {
                    lstHours.Items.Add(new Slot(slotStart, slotEnd));
                }

                start = start.Add(dur);
            }

            if (lstHours.Items.Count > 0)
                lstHours.SelectedIndex = 0;
        }


        private void btnSelect_Click(object sender, EventArgs e)
        {
            // Validation νέου πελάτη
            if (_isNewCustomer)
            {
                var name = txtName.Text.Trim();
                var phone = txtPhone.Text.Trim();

                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Πρέπει να γράψεις ονοματεπώνυμο.", "Προσοχή");
                    return;
                }

                if (string.IsNullOrWhiteSpace(phone))
                {
                    MessageBox.Show("Πρέπει να γράψεις τηλέφωνο.", "Προσοχή");
                    return;
                }

                if (!phone.All(char.IsDigit))
                {
                    MessageBox.Show("Το τηλέφωνο πρέπει να έχει μόνο αριθμούς.", "Προσοχή");
                    return;
                }

                if (phone.Length < 10)
                {
                    MessageBox.Show("Το τηλέφωνο πρέπει να έχει τουλάχιστον 10 ψηφία.", "Προσοχή");
                    return;
                }
            }

            if (cmbService.SelectedItem is not Service svc)
            {
                MessageBox.Show("Διάλεξε εργασία.", "Προσοχή");
                return;
            }

            if (lstHours.SelectedItem is not Slot slot)
            {
                MessageBox.Show("Διάλεξε ώρα.", "Προσοχή");
                return;
            }

            // User A/B
            SelectedUserCode = (cmbUser.SelectedItem?.ToString() ?? "A");

            SelectedServiceId = svc.Id;
            SelectedServiceName = svc.Name;
            SelectedStart = slot.Start;
            SelectedEnd = slot.End;

            DialogResult = DialogResult.OK;
            Close();
        }

        // Τηλέφωνο: μόνο αριθμοί + control keys
        private void TxtPhone_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        // Εσωτερική κλάση για εμφάνιση ωρών
        private class Slot
        {
            public DateTime Start { get; }
            public DateTime End { get; }
            public string Display => $"{Start:HH:mm} – {End:HH:mm}";

            public Slot(DateTime start, DateTime end)
            {
                Start = start;
                End = end;
            }

            public override string ToString() => Display;
        }
    }
}
