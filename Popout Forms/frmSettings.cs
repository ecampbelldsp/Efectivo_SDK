using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace eSSP_example
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            if ((ports == null) || (ports.Length == 0))
            {
                MessageBox.Show("No Serial Port found!", "ERROR");
                base.Dispose();
            }
            else
            {
                Array.Sort<string>(ports);
                for (byte i = 0; i < ports.Length; i = (byte)(i + 1))
                {
                    this.cbComPortVal1.Items.Add(ports[i]);
                    this.cbComPortVal2.Items.Add(ports[i]);
                }
                // Com Ports
                this.cbComPortVal1.SelectedItem = Global.ValidatorComPort;

                // SSP Addresses
                this.tbSSP1.Text = Global.Validator1SSPAddress.ToString();
                this.tbSSP2.Text = Global.Validator2SSPAddress.ToString();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Dispose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Save Com Port strings
            Properties.Settings.Default.ComPortVal1 = this.cbComPortVal1.Text;

            // Save SSP as bytes
            byte b1, b2;
            try
            {
                b1 = Byte.Parse (this.tbSSP1.Text);
                b2 = Byte.Parse (this.tbSSP2.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show (ex.ToString (), "EXCEPTION");
                return;
            }
            Properties.Settings.Default.SSP1 = b1;
            Properties.Settings.Default.SSP2 = b2;

            // Persist settings
            Properties.Settings.Default.Save();

            // Set in global
            Global.ValidatorComPort = Properties.Settings.Default.ComPortVal1;
            Global.Validator1SSPAddress = b1;
            Global.Validator2SSPAddress = b2;

            // Close form
            base.Dispose();
        }
    }
}
