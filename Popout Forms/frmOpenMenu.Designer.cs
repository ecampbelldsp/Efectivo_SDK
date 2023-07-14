namespace eSSP_example
{
    partial class frmOpenMenu
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
            this.cbComPortVal1 = new System.Windows.Forms.ComboBox();
            this.tbSSPAddressVal1 = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSSPAddressVal2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbComPortVal1
            // 
            this.cbComPortVal1.FormattingEnabled = true;
            this.cbComPortVal1.Location = new System.Drawing.Point(87, 57);
            this.cbComPortVal1.Name = "cbComPortVal1";
            this.cbComPortVal1.Size = new System.Drawing.Size(323, 21);
            this.cbComPortVal1.TabIndex = 0;
            // 
            // tbSSPAddressVal1
            // 
            this.tbSSPAddressVal1.Location = new System.Drawing.Point(87, 25);
            this.tbSSPAddressVal1.Name = "tbSSPAddressVal1";
            this.tbSSPAddressVal1.Size = new System.Drawing.Size(121, 20);
            this.tbSSPAddressVal1.TabIndex = 1;
            this.tbSSPAddressVal1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSSPAddress_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(85, 84);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(325, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "&Connect";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Com Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "SSP Address";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(214, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "SSP Address";
            // 
            // tbSSPAddressVal2
            // 
            this.tbSSPAddressVal2.Location = new System.Drawing.Point(289, 25);
            this.tbSSPAddressVal2.Name = "tbSSPAddressVal2";
            this.tbSSPAddressVal2.Size = new System.Drawing.Size(121, 20);
            this.tbSSPAddressVal2.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(84, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "SMART Payout";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(286, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Hopper";
            // 
            // frmOpenMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 127);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbSSPAddressVal2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.tbSSPAddressVal1);
            this.Controls.Add(this.cbComPortVal1);
            this.Name = "frmOpenMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Example Software";
            this.Load += new System.EventHandler(this.frmOpenMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbComPortVal1;
        private System.Windows.Forms.TextBox tbSSPAddressVal1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSSPAddressVal2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}