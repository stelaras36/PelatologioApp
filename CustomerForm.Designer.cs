namespace PelatologioApp
{
    partial class CustomerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtFullName = new TextBox();
            label2 = new Label();
            txtPhone = new TextBox();
            label3 = new Label();
            cmbRating = new ComboBox();
            label4 = new Label();
            txtNotes = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(95, 42);
            label1.Name = "label1";
            label1.Size = new Size(127, 20);
            label1.TabIndex = 0;
            label1.Text = "Ονοματεπώνυμο:";
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(250, 45);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(260, 27);
            txtFullName.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(95, 95);
            label2.Name = "label2";
            label2.Size = new Size(82, 20);
            label2.TabIndex = 2;
            label2.Text = "Τηλέφωνο:";
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(250, 92);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(160, 27);
            txtPhone.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(95, 139);
            label3.Name = "label3";
            label3.Size = new Size(55, 20);
            label3.TabIndex = 4;
            label3.Text = "Rating:";
            // 
            // cmbRating
            // 
            cmbRating.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRating.FormattingEnabled = true;
            cmbRating.Location = new Point(250, 139);
            cmbRating.Name = "cmbRating";
            cmbRating.Size = new Size(151, 28);
            cmbRating.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(95, 187);
            label4.Name = "label4";
            label4.Size = new Size(88, 20);
            label4.TabIndex = 6;
            label4.Text = "Σημειώσεις:";
            // 
            // txtNotes
            // 
            txtNotes.Location = new Point(250, 180);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new Size(260, 80);
            txtNotes.TabIndex = 7;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(196, 315);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(109, 29);
            btnSave.TabIndex = 8;
            btnSave.Text = "Αποθήκευση";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(363, 319);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Άκυρο";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // CustomerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtNotes);
            Controls.Add(label4);
            Controls.Add(cmbRating);
            Controls.Add(label3);
            Controls.Add(txtPhone);
            Controls.Add(label2);
            Controls.Add(txtFullName);
            Controls.Add(label1);
            Name = "CustomerForm";
            Text = "CustomerForm";
            Load += CustomerForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtFullName;
        private Label label2;
        private TextBox txtPhone;
        private Label label3;
        private ComboBox cmbRating;
        private Label label4;
        private TextBox txtNotes;
        private Button btnSave;
        private Button btnCancel;
    }
}