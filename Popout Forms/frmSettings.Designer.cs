namespace eSSP_example
{
    partial class frmSettings
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
            this.cbComPortVal1 = new System.Windows.Forms.ComboBox ();
            this.label1 = new System.Windows.Forms.Label ();
            this.btnOK = new System.Windows.Forms.Button ();
            this.btnCancel = new System.Windows.Forms.Button ();
            this.label2 = new System.Windows.Forms.Label ();
            this.cbComPortVal2 = new System.Windows.Forms.ComboBox ();
            this.label3 = new System.Windows.Forms.Label ();
            this.label4 = new System.Windows.Forms.Label ();
            this.tbSSP1 = new System.Windows.Forms.TextBox ();
            this.tbSSP2 = new System.Windows.Forms.TextBox ();
            this.label5 = new System.Windows.Forms.Label ();
            this.label6 = new System.Windows.Forms.Label ();
            this.SuspendLayout ();
            // 
            // cbComPortVal1
            // 
            this.cbComPortVal1.FormattingEnabled = true;
            this.cbComPortVal1.Location = new System.Drawing.Point (68, 35);
            this.cbComPortVal1.Name = "cbComPortVal1";
            this.cbComPortVal1.Size = new System.Drawing.Size (74, 21);
            this.cbComPortVal1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point (12, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size (50, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Com Port";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point (12, 188);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size (75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&Ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler (this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point (106, 188);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size (75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler (this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point (12, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size (50, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Com Port";
            // 
            // cbComPortVal2
            // 
            this.cbComPortVal2.FormattingEnabled = true;
            this.cbComPortVal2.Location = new System.Drawing.Point (68, 118);
            this.cbComPortVal2.Name = "cbComPortVal2";
            this.cbComPortVal2.Size = new System.Drawing.Size (74, 21);
            this.cbComPortVal2.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point (65, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size (74, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Note Validator";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point (75, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size (42, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Hopper";
            // 
            // tbSSP1
            // 
            this.tbSSP1.Location = new System.Drawing.Point (68, 62);
            this.tbSSP1.Name = "tbSSP1";
            this.tbSSP1.Size = new System.Drawing.Size (74, 20);
            this.tbSSP1.TabIndex = 8;
            // 
            // tbSSP2
            // 
            this.tbSSP2.Location = new System.Drawing.Point (68, 145);
            this.tbSSP2.Name = "tbSSP2";
            this.tbSSP2.Size = new System.Drawing.Size (74, 20);
            this.tbSSP2.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point (17, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size (45, 26);
            this.label5.TabIndex = 10;
            this.label5.Text = "SSP\r\nAddress";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point (17, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size (45, 26);
            this.label6.TabIndex = 11;
            this.label6.Text = "SSP\r\nAddress";
            // 
            // frmSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size (193, 223);
            this.Controls.Add (this.label6);
            this.Controls.Add (this.label5);
            this.Controls.Add (this.tbSSP2);
            this.Controls.Add (this.tbSSP1);
            this.Controls.Add (this.label4);
            this.Controls.Add (this.label3);
            this.Controls.Add (this.label2);
            this.Controls.Add (this.cbComPortVal2);
            this.Controls.Add (this.btnCancel);
            this.Controls.Add (this.btnOK);
            this.Controls.Add (this.label1);
            this.Controls.Add (this.cbComPortVal1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmSettings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Settings";
            this.Load += new System.EventHandler (this.frmSettings_Load);
            this.ResumeLayout (false);
            this.PerformLayout ();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbComPortVal1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbComPortVal2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbSSP1;
        private System.Windows.Forms.TextBox tbSSP2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}