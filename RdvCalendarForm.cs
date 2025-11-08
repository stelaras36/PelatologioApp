using System;
using System.Windows.Forms;
using PelatologioApp.Data;
using PelatologioApp.Models;

namespace PelatologioApp
{
    public partial class RdvCalendarForm : Form
    {
        private readonly AppointmentRepository _apptRepo;
        private readonly CustomerRepository _custRepo;

        public RdvCalendarForm(AppointmentRepository apptRepo, CustomerRepository custRepo)
        {
            InitializeComponent();
            _apptRepo = apptRepo;
            _custRepo = custRepo;

            InitGrid();
            InitRangeDropdown();
            LoadToday();
        }

        // =========================
        //   ΡΥΘΜΙΣΗ ΠΙΝΑΚΑ (GRID)
        // =========================
        private void InitGrid()
        {
            dgvRdv.AutoGenerateColumns = false;
            dgvRdv.Columns.Clear();

            // Ημερομηνία & ώρα
            dgvRdv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Start",
                HeaderText = "Ημερομηνία & Ώρα",
                Width = 160
            });

            // Ονοματεπώνυμο (από CustomerId, συμπληρώνεται με CellFormatting)
            dgvRdv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CustomerName",
                HeaderText = "Ονοματεπώνυμο",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            });

            // Τηλέφωνο (από CustomerId)
            dgvRdv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Phone",
                HeaderText = "Τηλέφωνο",
                Width = 120,
                ReadOnly = true
            });

            // Εργασία
            dgvRdv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ServiceName",
                HeaderText = "Εργασία",
                Width = 140
            });

            // Χρήστης A/B
            dgvRdv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UserCode",
                HeaderText = "Χρήστης",
                Width = 60
            });

            // Κατάσταση (booked, cancelled κλπ)
            dgvRdv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                HeaderText = "Κατάσταση",
                Width = 90
            });

            dgvRdv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRdv.MultiSelect = false;
            dgvRdv.ReadOnly = true;

            dgvRdv.CellDoubleClick += dgvRdv_CellDoubleClick;
            dgvRdv.CellFormatting += dgvRdv_CellFormatting;
        }

        // Συμπλήρωση Ονοματεπώνυμου & Τηλεφώνου από το CustomerId
        private void dgvRdv_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvRdv.Rows[e.RowIndex].DataBoundItem is not Appointment appt) return;

            var col = dgvRdv.Columns[e.ColumnIndex];

            if (col.Name == "CustomerName" || col.Name == "Phone")
            {
                var cust = _custRepo.GetById(appt.CustomerId);
                if (cust == null) return;

                if (col.Name == "CustomerName")
                    e.Value = cust.FullName;
                else if (col.Name == "Phone")
                    e.Value = cust.Phone;

                e.FormattingApplied = true;
            }
        }

        // =========================
        //   ΦΙΛΤΡΟ ΗΜΕΡΟΜΗΝΙΩΝ
        // =========================
        private void InitRangeDropdown()
        {
            cmbRange.Items.Clear();
            cmbRange.Items.AddRange(new object[]
            {
                "Σήμερα",
                "Αυτή την εβδομάδα",
                "Αυτόν τον μήνα"
            });

            cmbRange.SelectedIndex = 0;
            cmbRange.SelectedIndexChanged += cmbRange_SelectedIndexChanged;
        }

        private void cmbRange_SelectedIndexChanged(object? sender, EventArgs e)
        {
            switch (cmbRange.SelectedIndex)
            {
                case 0:
                    LoadToday();
                    break;
                case 1:
                    LoadWeek();
                    break;
                case 2:
                    LoadMonth();
                    break;
            }
        }

        private void LoadToday()
        {
            var from = DateTime.Today;
            var to = from.AddDays(1);
            dgvRdv.DataSource = _apptRepo.GetBetween(from, to);
        }

        private void LoadWeek()
        {
            var from = DateTime.Today;
            var to = from.AddDays(7);
            dgvRdv.DataSource = _apptRepo.GetBetween(from, to);
        }

        private void LoadMonth()
        {
            var from = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var to = from.AddMonths(1);
            dgvRdv.DataSource = _apptRepo.GetBetween(from, to);
        }

        // =========================
        //   ΔΙΠΛΟ ΚΛΙΚ ΣΕ RDV
        // =========================
        private void dgvRdv_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvRdv.Rows[e.RowIndex].DataBoundItem is not Appointment a) return;

            var res = MessageBox.Show(
                $"Ακύρωση ραντεβού στις {a.Start:dd/MM HH:mm};",
                "Επιβεβαίωση",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (res != DialogResult.Yes) return;

            var freeStart = a.Start;
            var freeEnd = a.End;

            _apptRepo.Delete(a.Id);

            // Λίστα αναμονής
            var candidates = _apptRepo.GetEarlierCandidates(freeStart);
            foreach (var cand in candidates)
            {
                var cust = _custRepo.GetById(cand.CustomerId);
                if (cust == null) continue;

                var label = _custRepo.GetRatingLabel(cust.Rating);
                var pref = cand.PreferredBefore.HasValue
                    ? cand.PreferredBefore.Value.ToString(@"hh\:mm")
                    : "-";

                var msg =
                    $"Ο πελάτης {cust.FullName} ({label}) έχει ραντεβού στις {cand.Start:dd/MM HH:mm} " +
                    $"και ζήτησε νωρίτερα (έως {pref}).\n" +
                    $"Θες να τον μεταφέρουμε στο νέο κενό {freeStart:dd/MM HH:mm};";

                var ans = MessageBox.Show(
                    msg,
                    "Λίστα αναμονής",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (ans == DialogResult.Yes)
                {
                    try
                    {
                        _apptRepo.UpdateTime(cand.Id, freeStart, freeEnd, clearEarlier: true);
                        MessageBox.Show(
                            $"Το ραντεβού του {cust.FullName} μεταφέρθηκε επιτυχώς.",
                            "Ενημέρωση",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Σφάλμα");
                    }

                    break;
                }
            }

            // Refresh τρέχοντος range
            switch (cmbRange.SelectedIndex)
            {
                case 0:
                    LoadToday();
                    break;
                case 1:
                    LoadWeek();
                    break;
                case 2:
                    LoadMonth();
                    break;
            }
        }

        // Dummy handler για να μην γκρινιάζει ο Designer αν υπάρχει παλιό hook
        private void dgvRdv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // intentionally empty
        }
    }
}
