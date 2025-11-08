namespace PelatologioApp
{
    partial class AppointmentForm
    {
        private System.ComponentModel.IContainer components = null;

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
            this.labelName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.labelPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.cmbUser = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbService = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.lstHours = new System.Windows.Forms.ListBox();
            this.chkWantsEarlier = new System.Windows.Forms.CheckBox();
            this.cmbPreferredTime = new System.Windows.Forms.ComboBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(30, 20);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(111, 20);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Ονοματεπώνυμο:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(160, 17);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(260, 27);
            this.txtName.TabIndex = 1;
            // 
            // labelPhone
            // 
            this.labelPhone.AutoSize = true;
            this.labelPhone.Location = new System.Drawing.Point(30, 60);
            this.labelPhone.Name = "labelPhone";
            this.labelPhone.Size = new System.Drawing.Size(78, 20);
            this.labelPhone.TabIndex = 2;
            this.labelPhone.Text = "Τηλέφωνο:";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(160, 57);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(260, 27);
            this.txtPhone.TabIndex = 2;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(30, 100);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(70, 20);
            this.lblUser.TabIndex = 3;
            this.lblUser.Text = "Χρήστης:";
            // 
            // cmbUser
            // 
            this.cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUser.FormattingEnabled = true;
            this.cmbUser.Location = new System.Drawing.Point(160, 97);
            this.cmbUser.Name = "cmbUser";
            this.cmbUser.Size = new System.Drawing.Size(80, 28);
            this.cmbUser.TabIndex = 3;
            // 
            // label1 (Εργασία)
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Εργασία:";
            // 
            // cmbService
            // 
            this.cmbService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbService.FormattingEnabled = true;
            this.cmbService.Location = new System.Drawing.Point(160, 142);
            this.cmbService.Name = "cmbService";
            this.cmbService.Size = new System.Drawing.Size(260, 28);
            this.cmbService.TabIndex = 4;
            // 
            // label2 (Ημερομηνία)
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Ημερομηνία:";
            // 
            // dtDate
            // 
            this.dtDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDate.Location = new System.Drawing.Point(160, 182);
            this.dtDate.Name = "dtDate";
            this.dtDate.Size = new System.Drawing.Size(200, 27);
            this.dtDate.TabIndex = 5;
            // 
            // label3 (Ώρα)
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 225);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Ώρα:";
            // 
            // lstHours
            // 
            this.lstHours.FormattingEnabled = true;
            this.lstHours.ItemHeight = 20;
            this.lstHours.Location = new System.Drawing.Point(160, 225);
            this.lstHours.Name = "lstHours";
            this.lstHours.Size = new System.Drawing.Size(120, 124);
            this.lstHours.TabIndex = 6;
            // 
            // chkWantsEarlier
            // 
            this.chkWantsEarlier.AutoSize = true;
            this.chkWantsEarlier.Location = new System.Drawing.Point(310, 225);
            this.chkWantsEarlier.Name = "chkWantsEarlier";
            this.chkWantsEarlier.Size = new System.Drawing.Size(219, 24);
            this.chkWantsEarlier.TabIndex = 7;
            this.chkWantsEarlier.Text = "Θέλει νωρίτερα αν αδειάσει";
            this.chkWantsEarlier.UseVisualStyleBackColor = true;
            // 
            // cmbPreferredTime
            // 
            this.cmbPreferredTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPreferredTime.FormattingEnabled = true;
            this.cmbPreferredTime.Items.AddRange(new object[] {
            "Δεν έχει προτίμηση",
            "Πριν τις 14:00",
            "Μετά τις 17:00",
            "Μετά τις 18:00"});
            this.cmbPreferredTime.Location = new System.Drawing.Point(310, 255);
            this.cmbPreferredTime.Name = "cmbPreferredTime";
            this.cmbPreferredTime.Size = new System.Drawing.Size(180, 28);
            this.cmbPreferredTime.TabIndex = 8;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(310, 305);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(180, 40);
            this.btnSelect.TabIndex = 9;
            this.btnSelect.Text = "Καταχώρηση";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // AppointmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 370);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.cmbPreferredTime);
            this.Controls.Add(this.chkWantsEarlier);
            this.Controls.Add(this.lstHours);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbService);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbUser);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.labelPhone);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.labelName);
            this.Name = "AppointmentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ραντεβού";
            this.Load += new System.EventHandler(this.AppointmentForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label labelPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.ComboBox cmbUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbService;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lstHours;
        private System.Windows.Forms.CheckBox chkWantsEarlier;
        private System.Windows.Forms.ComboBox cmbPreferredTime;
        private System.Windows.Forms.Button btnSelect;
    }
}
