using System;
using System.Windows.Forms;
using PelatologioApp.Models;

namespace PelatologioApp
{
    public partial class CustomerForm : Form
    {
        // Αν τη χρησιμοποιήσουμε για επεξεργασία, περνάμε existing customer
        private readonly Customer? _existing;

        // Το αποτέλεσμα μετά το Save (για edit)
        public Customer Result { get; private set; } = new Customer();

        // Γρήγορα accessors για σενάριο "νέος πελάτης" (όπως στο Form1)
        public string FullName => txtFullName.Text.Trim();
        public string Phone => txtPhone.Text.Trim();
        public string Notes => txtNotes.Text.Trim();
        public int Rating
        {
            get
            {
                if (cmbRating.SelectedItem != null &&
                    int.TryParse(cmbRating.SelectedItem.ToString(), out var r))
                {
                    if (r < -1) r = -1;
                    if (r > 1) r = 1;
                    return r;
                }
                return 0;
            }
        }

        public CustomerForm(Customer? existing = null)
        {
            InitializeComponent();
            _existing = existing;

            // Αν δεν τα έχεις δέσει από Designer, τα κρατάμε εδώ
            Load += CustomerForm_Load;
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;

            // Combo Rating (-1 / 0 / 1)
            cmbRating.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRating.Items.Clear();
            cmbRating.Items.Add("-1");
            cmbRating.Items.Add("0");
            cmbRating.Items.Add("1");
            cmbRating.SelectedIndex = 1; // default 0 (ουδέτερος)
        }

        private void CustomerForm_Load(object? sender, EventArgs e)
        {
            if (_existing != null)
            {
                // Φόρτωση δεδομένων για edit
                txtFullName.Text = _existing.FullName;
                txtPhone.Text = _existing.Phone;
                txtNotes.Text = _existing.Notes;

                var idx = 1; // 0 by default
                if (_existing.Rating == -1) idx = 0;
                else if (_existing.Rating == 1) idx = 2;
                cmbRating.SelectedIndex = idx;

                Result = new Customer
                {
                    Id = _existing.Id,
                    FullName = _existing.FullName,
                    Phone = _existing.Phone,
                    Notes = _existing.Notes,
                    Rating = _existing.Rating
                };
            }

            txtFullName.Focus();
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            var name = txtFullName.Text.Trim();
            var phone = txtPhone.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Όνομα απαιτείται.", "Προσοχή",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return;
            }

            if (phone.Length < 6)
            {
                MessageBox.Show("Γράψε ένα έγκυρο τηλέφωνο.", "Προσοχή",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return;
            }

            var rating = Rating; // από το property

            // Αν είμαστε σε edit mode, κρατάμε το ίδιο Id
            if (_existing != null)
            {
                Result = new Customer
                {
                    Id = _existing.Id,
                    FullName = name,
                    Phone = phone,
                    Notes = Notes,
                    Rating = rating
                };
            }
            else
            {
                // Νέος πελάτης (το Id θα το βάλει το Repository)
                Result = new Customer
                {
                    FullName = name,
                    Phone = phone,
                    Notes = Notes,
                    Rating = rating
                };
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
