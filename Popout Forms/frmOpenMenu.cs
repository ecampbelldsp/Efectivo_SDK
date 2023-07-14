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
    public partial class frmOpenMenu : Form
    {
        string[] m_ComPorts;
        Form1 m_Parent;

        public frmOpenMenu(Form1 frm)
        {
            InitializeComponent();
            m_Parent = frm;
            if (SearchForComPorts() > 0)
            {
                cbComPortVal1.Items.AddRange(m_ComPorts);
                cbComPortVal1.Text = Properties.Settings.Default.ComPortVal1;
                tbSSPAddressVal1.Text = Properties.Settings.Default.SSP1.ToString();
                tbSSPAddressVal2.Text = Properties.Settings.Default.SSP2.ToString ();
            }
            else
            {
                MessageBox.Show("No com ports found!", "ERROR");
                Application.Exit();
            }
        }

        public int SearchForComPorts()
        {
            m_ComPorts = SerialPort.GetPortNames();
            return m_ComPorts.Length;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbSSPAddressVal1.Text != "")
                {
                    Global.ValidatorComPort = cbComPortVal1.SelectedItem.ToString();
                    Global.Validator1SSPAddress = Byte.Parse(tbSSPAddressVal1.Text);
                    Global.Validator2SSPAddress = Byte.Parse(tbSSPAddressVal2.Text);
                    m_Parent.Show();
                    this.Close();
                    m_Parent.MainLoop();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "EXCEPTION");
            }
        }

        private void tbSSPAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                btnSearch_Click(sender, e);
        }

        private void frmOpenMenu_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }
    }
}
