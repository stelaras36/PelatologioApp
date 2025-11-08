using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using PelatologioApp.Data;
using PelatologioApp.Models;

namespace PelatologioApp
{
    public partial class Form1 : Form
    {
        private readonly CustomerRepository _custRepo = new();
        private readonly AppointmentRepository _apptRepo = new();

        private List<Customer> _currentList = new();
        private Customer? _selected;

        public Form1()
        {
            InitializeComponent();

            MessageBox.Show(
                "Τα δεδομένα αποθηκεύονται στον φάκελο:\n" + FileStorage.GetDataDir(),
                "Φάκελος αποθήκευσης",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitGrid();

            btnFind.Click += BtnFind_Click;
            dgvCustomers.CellDoubleClick += DgvCustomers_CellDoubleClick;
            dgvCustomers.CellFormatting += dgvCustomers_CellFormatting;
            btnCalendar.Click += btnCalendar_Click;
            txtLast4.KeyPress += TxtLast4_KeyPress;
        }

        private void InitGrid()
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.Columns.Clear();

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FullName",
                HeaderText = "Όνομα",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Phone",
                HeaderText = "Τηλέφωνο",
                Width = 120
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Rating",
                HeaderText = "Χαρ.",
                Width = 60
            });

            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.MultiSelect = false;
            dgvCustomers.ReadOnly = true;
        }

        // Χρώμα μόνο στη στήλη "Χαρ." (και όταν είναι επιλεγμένο)
        private void dgvCustomers_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var column = dgvCustomers.Columns[e.ColumnIndex];
            if (column.DataPropertyName != "Rating") return;

            if (dgvCustomers.Rows[e.RowIndex].DataBoundItem is not Customer c)
                return;

            Color? backColor = null;

            switch (c.Rating)
            {
                case 1:
                    backColor = Color.LightGreen;   // καλός
                    break;
                case 0:
                    backColor = Color.LightBlue;    // ουδέτερος
                    break;
                case -1:
                    backColor = Color.LightCoral;   // "δύσκολος"
                    break;
            }

            if (backColor.HasValue)
            {
                e.CellStyle.BackColor = backColor.Value;
                e.CellStyle.SelectionBackColor = backColor.Value;
                e.CellStyle.ForeColor = Color.Black;
                e.CellStyle.SelectionForeColor = Color.Black;
            }
        }

        private void BtnFind_Click(object? sender, EventArgs e)
        {
            var last4 = txtLast4.Text.Trim();
            if (string.IsNullOrWhiteSpace(last4))
            {
                MessageBox.Show("Βάλε τα τελευταία 4 ψηφία.", "Προσοχή");
                return;
            }

            _currentList = _custRepo.FindByLast4(last4);
            dgvCustomers.DataSource = _currentList;
            _selected = _currentList.FirstOrDefault();

            if (_currentList.Count == 0)
            {
                OpenAppointmentForNewCustomer(last4);
            }
        }

        private void DgvCustomers_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvCustomers.Rows[e.RowIndex].DataBoundItem is not Customer c)
                return;

            OpenAppointmentForExistingCustomer(c);
        }

        private void OpenAppointmentForExistingCustomer(Customer c)
        {
            using var f = new AppointmentForm(c);
            if (f.ShowDialog(this) != DialogResult.OK) return;

            var appt = new Appointment
            {
                CustomerId = c.Id,
                Start = f.SelectedStart,
                End = f.SelectedEnd,
                ServiceId = f.SelectedServiceId,
                ServiceName = f.SelectedServiceName,
                Status = "booked",
                WantsEarlier = f.WantsEarlier,
                PreferredBefore = f.PreferredBefore,
                UserCode = f.SelectedUserCode
            };

            try
            {
                _apptRepo.Insert(appt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Σφάλμα");
            }
        }

        private void OpenAppointmentForNewCustomer(string last4)
        {
            using var f = new AppointmentForm();
            if (f.ShowDialog(this) != DialogResult.OK) return;

            var cust = new Customer
            {
                FullName = f.CustomerName,
                Phone = f.CustomerPhone,
                Notes = "",
                Rating = 0
            };

            cust = _custRepo.Upsert(cust);

            var appt = new Appointment
            {
                CustomerId = cust.Id,
                Start = f.SelectedStart,
                End = f.SelectedEnd,
                ServiceId = f.SelectedServiceId,
                ServiceName = f.SelectedServiceName,
                Status = "booked",
                WantsEarlier = f.WantsEarlier,
                PreferredBefore = f.PreferredBefore,
                UserCode = f.SelectedUserCode
            };

            try
            {
                _apptRepo.Insert(appt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Σφάλμα");
            }

            _currentList = new List<Customer> { cust };
            dgvCustomers.DataSource = _currentList;
            _selected = cust;
        }

        private void btnCalendar_Click(object sender, EventArgs e)
        {
            using var cal = new RdvCalendarForm(_apptRepo, _custRepo);
            cal.ShowDialog(this);
        }

        private void TxtLast4_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            Customer? selected = null;

            if (dgvCustomers.CurrentRow?.DataBoundItem is Customer c1)
                selected = c1;
            else if (_selected != null)
                selected = _selected;

            if (selected == null)
            {
                MessageBox.Show("Διάλεξε έναν πελάτη από τη λίστα.", "Προσοχή",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var f = new CustomerForm(selected);
            if (f.ShowDialog(this) != DialogResult.OK)
                return;

            var updated = f.Result;
            _custRepo.Upsert(updated);

            var idx = _currentList.FindIndex(x => x.Id == updated.Id);
            if (idx >= 0)
                _currentList[idx] = updated;

            dgvCustomers.DataSource = null;
            dgvCustomers.DataSource = _currentList;
            _selected = updated;
        }
    }
}
