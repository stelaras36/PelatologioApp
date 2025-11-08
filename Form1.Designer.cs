namespace PelatologioApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblLast4 = new Label();
            txtLast4 = new TextBox();
            btnFind = new Button();
            dgvCustomers = new DataGridView();
            btnCalendar = new Button();
            btnEditCustomer = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            SuspendLayout();
            // 
            // lblLast4
            // 
            lblLast4.AutoSize = true;
            lblLast4.Location = new Point(65, 42);
            lblLast4.Name = "lblLast4";
            lblLast4.Size = new Size(137, 20);
            lblLast4.TabIndex = 0;
            lblLast4.Text = "Τελευταία 4 ψηφία:";
            // 
            // txtLast4
            // 
            txtLast4.Location = new Point(231, 35);
            txtLast4.MaxLength = 4;
            txtLast4.Name = "txtLast4";
            txtLast4.Size = new Size(80, 27);
            txtLast4.TabIndex = 1;
            // 
            // btnFind
            // 
            btnFind.Location = new Point(383, 35);
            btnFind.Name = "btnFind";
            btnFind.Size = new Size(94, 29);
            btnFind.TabIndex = 2;
            btnFind.Text = "Αναζήτηση";
            btnFind.UseVisualStyleBackColor = true;
            // 
            // dgvCustomers
            // 
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomers.Dock = DockStyle.Bottom;
            dgvCustomers.Location = new Point(0, 130);
            dgvCustomers.MultiSelect = false;
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.ReadOnly = true;
            dgvCustomers.RowHeadersWidth = 51;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.Size = new Size(800, 320);
            dgvCustomers.TabIndex = 4;
            // 
            // btnCalendar
            // 
            btnCalendar.Location = new Point(526, 38);
            btnCalendar.Name = "btnCalendar";
            btnCalendar.Size = new Size(104, 29);
            btnCalendar.TabIndex = 5;
            btnCalendar.Text = "Ημερολόγιο";
            btnCalendar.UseVisualStyleBackColor = true;
            btnCalendar.Click += btnCalendar_Click;
            // 
            // btnEditCustomer
            // 
            btnEditCustomer.Location = new Point(665, 41);
            btnEditCustomer.Name = "btnEditCustomer";
            btnEditCustomer.Size = new Size(109, 55);
            btnEditCustomer.TabIndex = 6;
            btnEditCustomer.Text = "Επεξεργασία πελάτη";
            btnEditCustomer.UseVisualStyleBackColor = true;
            btnEditCustomer.Click += btnEditCustomer_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnEditCustomer);
            Controls.Add(btnCalendar);
            Controls.Add(dgvCustomers);
            Controls.Add(btnFind);
            Controls.Add(txtLast4);
            Controls.Add(lblLast4);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblLast4;
        private TextBox txtLast4;
        private Button btnFind;
        private DataGridView dgvCustomers;
        private Button btnCalendar;
        private Button btnEditCustomer;
    }
}
