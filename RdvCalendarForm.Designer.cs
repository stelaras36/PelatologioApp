namespace PelatologioApp
{
    partial class RdvCalendarForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Καθαρισμός πόρων.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            dgvRdv = new DataGridView();
            cmbRange = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvRdv).BeginInit();
            SuspendLayout();
            // 
            // dgvRdv
            // 
            dgvRdv.AllowUserToAddRows = false;
            dgvRdv.AllowUserToDeleteRows = false;
            dgvRdv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvRdv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRdv.Location = new Point(12, 12);
            dgvRdv.MultiSelect = false;
            dgvRdv.Name = "dgvRdv";
            dgvRdv.ReadOnly = true;
            dgvRdv.RowHeadersWidth = 51;
            dgvRdv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRdv.Size = new Size(776, 383);
            dgvRdv.TabIndex = 0;
            dgvRdv.CellContentClick += dgvRdv_CellContentClick;
            dgvRdv.CellDoubleClick += dgvRdv_CellDoubleClick;
            // 
            // cmbRange
            // 
            cmbRange.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cmbRange.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRange.FormattingEnabled = true;
            cmbRange.Items.AddRange(new object[] { "Σήμερα", "Αυτή την εβδομάδα", "Αυτόν τον μήνα" });
            cmbRange.Location = new Point(12, 401);
            cmbRange.Name = "cmbRange";
            cmbRange.Size = new Size(776, 28);
            cmbRange.TabIndex = 1;
            cmbRange.SelectedIndexChanged += cmbRange_SelectedIndexChanged;
            // 
            // RdvCalendarForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 441);
            Controls.Add(cmbRange);
            Controls.Add(dgvRdv);
            Name = "RdvCalendarForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ημερολόγιο Ραντεβού";
            ((System.ComponentModel.ISupportInitialize)dgvRdv).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRdv;
        private System.Windows.Forms.ComboBox cmbRange;
    }
}
