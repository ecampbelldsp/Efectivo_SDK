using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using ITLlib;
using eSSP_example;

namespace eSSP_example.Pipeline
{
    public partial class BaseForm1 : Form
    {
        // Variables
        public bool hopperRunning = false, payoutRunning = false;
        volatile public bool hopperConnecting = false, payoutConnecting = false;
        int pollTimer = 250; // timer in ms
        BaseHopper Hopper; // The class that interfaces with the Hopper
        BasePayout Payout; // The class that interfaces with the Payout
        bool FormSetup = false; // To ensure the form is only set up on first run
        frmPayoutByDenom payoutByDenomFrm; // Payout by denomination form
        delegate void OutputMessage(string msg); // Delegate for invoking on cross thread calls
        Thread tHopRec, tSPRec; // Handles to each of the reconnection threads for the 2 units

        // Constructor
        public BaseForm1(BaseHopper Hopper_in, BasePayout Payout_in)
        {
            Hopper = Hopper_in;
            Payout = Payout_in;
            /*InitializeComponent();
            timer1.Interval = pollTimer;
            timer2.Interval = 500; // update UI every 500ms*/
            this.Location = new Point(Screen.PrimaryScreen.Bounds.X + 50, Screen.PrimaryScreen.Bounds.Y + 30);
            this.Enabled = false;
        }

        // This updates UI variables such as textboxes etc.
        /*void UpdateUI()
        {
            // Get stored notes info from SMART Payout and SMART Hopper at intervals
            if (!timer2.Enabled)
            {
                tbChannelLevels.Text = Payout.GetChannelLevelInfo();
                tbCoinLevels.Text = Hopper.GetChannelLevelInfo();
                timer2.Enabled = true;
            }
        }*/

        // The main program loop, this is to control the validator, it polls at
        // a value set in this class (pollTimer).
        public void startingConection()
        {
            Thread.Sleep(500);
            // Connect to the validators (non-threaded for initial connect)
            while (!payoutRunning && !hopperRunning)
            {
                ConnectToSMARTPayout();
                ConnectToHopper();

                // Enable validators
                Payout.EnableValidator();
                Hopper.EnableValidator();

                //Enable Coin Feeder
                Hopper.EnableCoinMech();
            }
            Console.WriteLine("Sistema de pago activado\n");
        }
        public void MainLoop()
        {
            this.Enabled = true;
            //btnRun.Enabled = false;
            //btnHalt.Enabled = true;

            Thread.Sleep(500);
            // Connect to the validators (non-threaded for initial connect)
            while (!payoutRunning && !hopperRunning)
            {
                ConnectToSMARTPayout();
                ConnectToHopper();

                // Enable validators
                Payout.EnableValidator();
                Hopper.EnableValidator();

                //Enable Coin Feeder
                 Hopper.EnableCoinMech();
            }
            Console.WriteLine("Sistema de pago activado\n");

            bool startRunningProcess = true;

            // While app active
            while (true)  //!CHelpers.Shutdown
            {

                // If the Hopper is supposed to be running but the poll fails
                if (hopperRunning && !hopperConnecting && !Hopper.DoPoll())
                {
                    //textBox1.AppendText("Lost connection to SMART Hopper\r\n");
                    // If the other unit isn't running, refresh the port by closing it
                    if (!payoutRunning) LibraryHandler.ClosePort();
                    hopperRunning = false;
                    // Create and start a reconnection thread, this allows this loop to continue executing
                    // and polling the other validator
                    tHopRec = new Thread(() => ReconnectHopper());
                    tHopRec.Start();
                }
                // Same as above but for the Payout
                if (payoutRunning && !payoutConnecting && !Payout.DoPoll())
                {
                    //textBox1.AppendText("Lost connection to SMART Payout\r\n");
                    // If the other unit isn't running, refresh the port by closing it
                    if (!hopperRunning) LibraryHandler.ClosePort();
                    payoutRunning = false;
                    // Create and start a reconnection thread, this allows this loop to continue executing
                    // and polling the other validator
                    tSPRec = new Thread(() => ReconnectPayout());
                    tSPRec.Start();
                }

                if (startRunningProcess)
                {
                    Console.WriteLine("Inserte dinero!!!\n");
                    //Console.ReadLine();
                    startRunningProcess = false;
                }
                /*
                char[] currency = { 'E', 'U', 'R' };
                string amount = "0.2";
                CalculatePayout(amount, currency);
                break;*/



                /* break;/**/
                /*UpdateUI();
                timer1.Enabled = true;*/
                //while (timer1.Enabled) Application.DoEvents();

                //CalculatePayout(tbPayout.Text, tbPayoutCurrency.Text.ToCharArray());
            }

            //btnRun.Enabled = true;
            //btnHalt.Enabled = false;
        }

        // This is a one off function that is called the first time the MainLoop()
        // function runs, it just sets up a few of the UI elements that only need
        // updating once.
        public float CountingPayment(float amountToPay)
        {
            startingConection();
            /*
            Thread.Sleep(500);
            // Connect to the validators (non-threaded for initial connect)
            while (!payoutRunning && !hopperRunning)
            {
                ConnectToSMARTPayout();
                ConnectToHopper();

                // Enable validators
                Payout.EnableValidator();
                Hopper.EnableValidator();

                //Enable Coin Feeder
                Hopper.EnableCoinMech();
            }
            Console.WriteLine("Sistema de pago activado");*/

            bool startRunningProcess = true;

            // While app active
            while (true)  //!CHelpers.Shutdown
            {

                // If the Hopper is supposed to be running but the poll fails
                if (hopperRunning && !hopperConnecting && !Hopper.DoPoll())
                {
                    // If the other unit isn't running, refresh the port by closing it
                    if (!payoutRunning) LibraryHandler.ClosePort();
                    hopperRunning = false;
                    // Create and start a reconnection thread, this allows this loop to continue executing
                    // and polling the other validator
                    tHopRec = new Thread(() => ReconnectHopper());
                    tHopRec.Start();
                }
                // Same as above but for the Payout
                if (payoutRunning && !payoutConnecting && !Payout.DoPoll())
                {
                    // If the other unit isn't running, refresh the port by closing it
                    if (!hopperRunning) LibraryHandler.ClosePort();
                    payoutRunning = false;
                    // Create and start a reconnection thread, this allows this loop to continue executing
                    // and polling the other validator
                    tSPRec = new Thread(() => ReconnectPayout());
                    tSPRec.Start();
                }
                Hopper.DoPoll();
                Payout.DoPoll();

                if (startRunningProcess)
                {
                    Console.WriteLine("Inserte dinero!!! \n");
                    startRunningProcess = false;
                }

                if (Global.countingPayment >= amountToPay)
                {

                    hopperRunning = false;
                    payoutRunning = false;

                    return Global.countingPayment - amountToPay;



                }

            }

        }

        public Response Pagar(string cashsBack)
        {
            startingConection();
            Response response = new Response();

            bool startRunningProcess = true;

            // While app active
            while (Global.NotePaymentActive || Global.CoinPaymentActive)  //!CHelpers.Shutdown
            {

                // If the Hopper is supposed to be running but the poll fails
                if (hopperRunning && !hopperConnecting && !Hopper.DoPoll())
                {
                    // If the other unit isn't running, refresh the port by closing it
                    if (!payoutRunning) LibraryHandler.ClosePort();
                    hopperRunning = false;
                    // Create and start a reconnection thread, this allows this loop to continue executing
                    // and polling the other validator
                    tHopRec = new Thread(() => ReconnectHopper());
                    tHopRec.Start();
                }
                // Same as above but for the Payout
                if (payoutRunning && !payoutConnecting && !Payout.DoPoll())
                {
                    // If the other unit isn't running, refresh the port by closing it
                    if (!hopperRunning) LibraryHandler.ClosePort();
                    payoutRunning = false;
                    // Create and start a reconnection thread, this allows this loop to continue executing
                    // and polling the other validator
                    tSPRec = new Thread(() => ReconnectPayout());
                    tSPRec.Start();
                }
                Hopper.DoPoll();
                Payout.DoPoll();

                if (startRunningProcess)
                {
                    Console.WriteLine("Iniciando devolución!!!");
                    startRunningProcess = false;
                    char[] currency = { 'E', 'U', 'R' };
                    response = CalculatePayout(cashsBack, currency);
                }

                                    
                    
                    
                    hopperRunning = false;
                    payoutRunning = false;

            }

            return response;
        }


        public void ConnectToHopper()
        {
            hopperConnecting = true;
            // setup timer, timeout delay and number of attempts to connect
            //System.Windows.Forms.Timer reconnectionTimer = new System.Windows.Forms.Timer();
            //reconnectionTimer.Tick += new EventHandler(reconnectionTimer_Tick);
           // reconnectionTimer.Interval = 1000; // ms
            int attempts = 1000;

            // Setup connection info
            Hopper.CommandStructure.ComPort = Global.ValidatorComPort;
            Hopper.CommandStructure.SSPAddress = Global.Validator2SSPAddress;
            Hopper.CommandStructure.BaudRate = 9600;
            Hopper.CommandStructure.Timeout = 1000;
            Hopper.CommandStructure.RetryLevel = 3;

            // Run for number of attempts specified
            for (int i = 0; i < attempts; i++)
            {
                //if (log != null) log.AppendText("Trying connection to SMART Hopper\r\n");

                // turn encryption off for first stage
                Hopper.CommandStructure.EncryptionStatus = false;

                // if the key negotiation is successful then set the rest up
                if (Hopper.OpenPort() && Hopper.NegotiateKeys())
                {
                    Hopper.CommandStructure.EncryptionStatus = true; // now encrypting
                    // find the max protocol version this validator supports
                    byte maxPVersion = FindMaxHopperProtocolVersion();
                    if (maxPVersion >= 7)
                        Hopper.SetProtocolVersion(maxPVersion);
                    else
                    {
                        //MessageBox.Show("This program does not support units under protocol 6!", "ERROR");
                        hopperConnecting = false;
                        return;
                    }
                    // get info from the hopper and store useful vars
                    Hopper.HopperSetupRequest();
                    // check the right unit is connected
                    if (!IsHopperSupported(Hopper.UnitType))
                    {
                        //MessageBox.Show("Unsupported type shown by SMART Hopper, this SDK supports the SMART Payout and the SMART Hopper only");
                        hopperConnecting = false;
                        //Application.Exit();
                        return;
                    }
                    // inhibits, this sets which channels can receive coins
                    Hopper.SetInhibits();
                    // Get serial number.
                    Hopper.GetSerialNumber();
                    // set running to true so the hopper begins getting polled
                    hopperRunning = true;
                    hopperConnecting = false;
                    return;
                }
                // reset timer
                //reconnectionTimer.Enabled = true;
                /*while (reconnectionTimer.Enabled)
                {
                    if (CHelpers.Shutdown)
                    {
                        hopperConnecting = false;
                        return;
                    }

                    //Application.DoEvents();
                    Thread.Sleep(1);
                }*/
            }
            hopperConnecting = false;
            return;
        }

        public void ConnectToSMARTPayout()
        {
            payoutConnecting = true;
            // setup timer, timeout delay and number of attempts to connect
            //System.Windows.Forms.Timer reconnectionTimer = new System.Windows.Forms.Timer();
           // reconnectionTimer.Tick += new EventHandler(reconnectionTimer_Tick);
            //reconnectionTimer.Interval = 1000; // ms
            int attempts = 1000;

            // Setup connection info
            Payout.CommandStructure.ComPort = Global.ValidatorComPort;
            Payout.CommandStructure.SSPAddress = Global.Validator1SSPAddress;
            Payout.CommandStructure.BaudRate = 9600;
            Payout.CommandStructure.Timeout = 1000;
            Payout.CommandStructure.RetryLevel = 3;

            // Run for number of attempts specified
            for (int i = 0; i < attempts; i++)
            {
                //if (log != null) log.AppendText("Trying connection to SMART Payout\r\n");

                // turn encryption off for first stage
                Payout.CommandStructure.EncryptionStatus = false;

                // Open port first, if the key negotiation is successful then set the rest up
                if (Payout.OpenPort() && Payout.NegotiateKeys())
                {
                    Payout.CommandStructure.EncryptionStatus = true; // now encrypting
                    // find the max protocol version this validator supports
                    byte maxPVersion = FindMaxPayoutProtocolVersion();
                    if (maxPVersion >= 6)
                        Payout.SetProtocolVersion(maxPVersion);
                    else
                    {
                        //MessageBox.Show("This program does not support units under protocol 6!", "ERROR");
                        payoutConnecting = false;
                        return;
                    }
                    // get info from the validator and store useful vars
                    Payout.PayoutSetupRequest();
                    // check the right unit is connected
                    if (!IsValidatorSupported(Payout.UnitType))
                    {
                        //MessageBox.Show("Unsupported type shown by SMART Payout, this SDK supports the SMART Payout and the SMART Hopper only");
                        payoutConnecting = false;
                        //Application.Exit();
                        return;
                    }
                    // inhibits, this sets which channels can receive notes
                    Payout.SetInhibits();
                    // Get serial number.
                    Payout.GetSerialNumber();
                    // enable payout
                    Payout.EnablePayout();
                    // set running to true so the validator begins getting polled
                    payoutRunning = true;
                    payoutConnecting = false;
                    return;
                }
                // reset timer
                //reconnectionTimer.Enabled = true;
                /*while (reconnectionTimer.Enabled)
                {
                    if (CHelpers.Shutdown)
                    {
                        payoutConnecting = false;
                        return;
                    }

                    //Application.DoEvents();
                    Thread.Sleep(1);
                }*/
            }
            payoutConnecting = false;
            return;
        }

        // Used when invoking for cross-thread calls
        private void AppendToTextBox(string s)
        {
            //textBox1.AppendText(s);
        }

        // These functions are run in a seperate thread to allow the main loop to continue executing.
        private void ReconnectHopper()
        {
            OutputMessage m = new OutputMessage(AppendToTextBox);
            while (!hopperRunning)
            {
               /* if (textBox1.InvokeRequired)
                    textBox1.Invoke(m, new object[] { "Attempting to reconnect to SMART Hopper...\r\n" });
                else
                    textBox1.AppendText("Attempting to reconnect to SMART Hopper...\r\n");
               */
                ConnectToHopper();
                CHelpers.Pause(1000);
                if (CHelpers.Shutdown) return;
            }
           /* if (textBox1.InvokeRequired)
                textBox1.Invoke(m, new object[] { "Reconnected to SMART Hopper\r\n" });
            else
                textBox1.AppendText("Reconnected to SMART Hopper\r\n");*/
            Hopper.EnableValidator();
        }

        private void ReconnectPayout()
        {
            OutputMessage m = new OutputMessage(AppendToTextBox);
            while (!payoutRunning)
            {
               /* if (textBox1.InvokeRequired)
                    textBox1.Invoke(m, new object[] { "Attempting to reconnect to SMART Payout...\r\n" });
                else
                    textBox1.AppendText("Attempting to reconnect to SMART Payout...\r\n");
               */
                ConnectToSMARTPayout(); // ConnectToSMARTPayout(null)    Have to pass null as can't update text box from a different thread without invoking

                CHelpers.Pause(1000);
                if (CHelpers.Shutdown) return;
            }
            /*if (textBox1.InvokeRequired)
                textBox1.Invoke(m, new object[] { "Reconnected to SMART Payout\r\n" });
            else
                textBox1.AppendText("Reconnected to SMART Payout\r\n");*/
            Payout.EnableValidator();
        }

        // This function finds the maximum protocol version that a validator supports. To do this
        // it attempts to set a protocol version starting at 6 in this case, and then increments the
        // version until error 0xF8 is returned from the validator which indicates that it has failed
        // to set it. The function then returns the version number one less than the failed version.
        private byte FindMaxHopperProtocolVersion()
        {
            // not dealing with protocol under level 6
            // attempt to set in hopper
            byte b = 0x07;
            while (true)
            {
                // If command can't get through, break out
                if (!Hopper.SetProtocolVersion(b)) break;
                // If it fails then it can't be set so fall back to previous iteration and return it
                if (Hopper.CommandStructure.ResponseData[0] == CCommands.SSP_RESPONSE_FAIL)
                    return --b;
                b++;

                if (b > 20)
                    return 0x07; // return default if protocol gets too high (must be failure)
            }
            return 0x07; // default
        }

        private byte FindMaxPayoutProtocolVersion()
        {
            // not dealing with protocol under level 6
            // attempt to set in validator
            byte b = 0x06;
            while (true)
            {
                Payout.SetProtocolVersion(b);
                // If it fails then it can't be set so fall back to previous iteration and return it
                if (Payout.CommandStructure.ResponseData[0] == CCommands.SSP_RESPONSE_FAIL)
                    return --b;
                b++;

                // If the protocol version 'runs away' because of a drop in comms. Return the default value.
                if (b > 20)
                    return 0x06;
            }
        }

        // This function shows a simple example of calculating a payout split between the SMART Payout and the 
        // SMART Hopper. It works on a highest value split, first the notes are looked at, then any remainder
        // that can't be paid out with a note is paid from the SMART Hopper.
        private Response CalculatePayout(string amount, char[] currency)
            /*
             return 1: can not parse string 
             */
        {   
            Response response = new Response();
            float payoutAmount;
            try
            {
                // Parse it to a number
                payoutAmount = float.Parse(amount) * 100;
            }
            catch (Exception ex)
            {
                response.set(false, "Cannot parse string payoutAmount to a number");
                //MessageBox.Show(ex.Message);
                return response;
            }

            int payoutList = 0;
            // Obtain the list of sorted channels from the SMART Payout, this is sorted by channel value
            // - lowest first
            List<ChannelData> reverseList = new List<ChannelData>(Payout.UnitDataList);
            reverseList.Reverse(); // Reverse the list so the highest value is first

            // Iterate through each
            foreach (ChannelData d in reverseList)
            {
                ChannelData temp = d; // Don't overwrite real values
                // Keep testing to see whether we need to payout this note or the next note
                while (true)
                {
                    // If the amount to payout is greater than the value of the current note and there is
                    // some of that note available and it is the correct currency
                    if (payoutAmount >= temp.Value && temp.Level > 0 && String.Equals(new String(temp.Currency), new String(currency)))
                    {
                        payoutList += temp.Value; // Add to the list of notes to payout from the SMART Payout
                        payoutAmount -= temp.Value; // Minus from the total payout amount
                        temp.Level--; // Take one from the level
                    }
                    else
                        break; // Don't need any more of this note
                }
            }

            // Test the proposed payout values
            if (payoutList > 0)
            {
                // First test SP
                Payout.PayoutAmount(payoutList, currency, true);
                if (Payout.CommandStructure.ResponseData[0] != 0xF0)
                {
                    /*DialogResult res =
                        MessageBox.Show("Smart Payout unable to pay requested amount, attempt to pay all from Hopper?",
                        "Error with Payout", MessageBoxButtons.YesNo);

                    if (res == System.Windows.Forms.DialogResult.No)
                        return;
                    else*/
                        payoutAmount += payoutList;
                }

                // Test Hopper first
                if (payoutAmount > 0) { 
                Hopper.PayoutAmount((int)payoutAmount, currency, true);
                    if (Hopper.CommandStructure.ResponseData[0] != 0xF0)
                    {
                        response.set(false, "Unable to pay requested amount using Hopper");
                        //MessageBox.Show("Unable to pay requested amount!");
                        return response;
                    }
                }


                // SP is ok to pay
                Payout.PayoutAmount(payoutList, currency, false);
                if (Payout.CommandStructure.ResponseData[0] != 0xF0)
                {
                    response.set(false, "Issue with Payout payment");
                    return response;
                }
            }

            // Now if there is any left over, request from Hopper
            if (payoutAmount > 0)
            {
                /*
                // Test Hopper first
                Hopper.PayoutAmount((int)payoutAmount, currency, true);
                if (Hopper.CommandStructure.ResponseData[0] != 0xF0)
                {
                    response.set(false, "Unable to pay requested amount using Hopper");
                    //MessageBox.Show("Unable to pay requested amount!");
                    return response;
                }
                */

                // Hopper is ok to pay
                Hopper.PayoutAmount((int)payoutAmount, currency, false);
                if (Hopper.CommandStructure.ResponseData[0] != 0xF0)
                {
                    response.set(false, "Issue with Hopper payment");
                    return response;
                }
            }
            response.set(true, "Successful payment");

            return response;
        }

        // This function checks whether the type of validator is supported by this program.
        private bool IsValidatorSupported(char type)
        {
            if (type == (char)0x06)
                return true;
            return false;
        }

        private bool IsHopperSupported(char type)
        {
            if (type == (char)0x03 || type == (char)0x09)
                return true;
            return false;
        }
        /* Events handling section */

        private void Form1_Load(object sender, EventArgs e)
        {
            // Create instances of the validator classes
            Hopper = new BaseHopper();
            Payout = new BasePayout();
            if (Hopper == null || Payout == null)
            {
                MessageBox.Show("Error with memory allocation, exiting", "ERROR");
                Application.Exit();
            }
            //btnHalt.Enabled = true;

            // Load settings
            //logTickBox.Checked = Properties.Settings.Default.Comms;

            // Position comms windows
            Point p = Location;
            p.Y += this.Height;
            Hopper.Comms.Location = p;
            p.X += Hopper.Comms.Width;
            Payout.CommsLog.Location = p;
        }

       /* private void Form1_Shown(object sender, EventArgs e)
        {
            frmOpenMenu f = new frmOpenMenu(this);
            f.Show();
            this.Hide();
        }*/


        /*
        private void btnRun_Click(object sender, EventArgs e)
        {
            textBox1.AppendText("Started poll loop\r\n");
            MainLoop();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formSettings = new frmSettings();
            formSettings.ShowDialog();
            hopperRunning = false;
            payoutRunning = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
        }

        private void btnHalt_Click(object sender, EventArgs e)
        {
            textBox1.AppendText("Poll loop stopped\r\n");
            hopperRunning = false;
            payoutRunning = false;
            btnRun.Enabled = true;
            btnHalt.Enabled = false;
        }

        private void logTickBox_CheckedChanged(object sender, EventArgs e)
        {
            if (logTickBox.Checked)
            {
                Hopper.Comms.Show();
                Payout.CommsLog.Show();
            }
            else
            {
                Hopper.Comms.Hide();
                Payout.CommsLog.Hide();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            hopperRunning = false;
            payoutRunning = false;
            CHelpers.Shutdown = true;
            LibraryHandler.ClosePort();
            Properties.Settings.Default.Comms = logTickBox.Checked;
            Properties.Settings.Default.ComPortVal1 = Global.ValidatorComPort;
            Properties.Settings.Default.SSP1 = Global.Validator1SSPAddress;
            Properties.Settings.Default.SSP2 = Global.Validator2SSPAddress;
            Properties.Settings.Default.Save();
        }

        private void btnPayout_Click(object sender, EventArgs e)
        {
            if (tbPayout.Text != "" && tbPayoutCurrency.Text != "")
                CalculatePayout(tbPayout.Text, tbPayoutCurrency.Text.ToCharArray());
        }

        private void btnEmptyHopper_Click(object sender, EventArgs e)
        {
            Hopper.EmptyDevice(textBox1);
        }

        private void btnSmartEmptyHopper_Click(object sender, EventArgs e)
        {
            Hopper.SmartEmpty(textBox1);
        }

        private void btnEmptySMARTPayout_Click(object sender, EventArgs e)
        {
            Payout.EmptyPayoutDevice(textBox1);
        }

        private void btnSMARTEmpty_Click(object sender, EventArgs e)
        {
            Payout.SmartEmpty(textBox1);
        }

        private void recycleBoxHopper_CheckedChange(object sender, EventArgs e)
        {
            CheckBox c = sender as CheckBox;
            try
            {
                if (c.Checked)
                    Hopper.RouteChannelToStorage(Int32.Parse(c.Name), textBox1);
                else
                    Hopper.RouteChannelToCashbox(Int32.Parse(c.Name), textBox1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        private void recycleBoxPayout_CheckedChange(object sender, EventArgs e)
        {
            CheckBox chkbox = sender as CheckBox;
            try
            {
                ChannelData d = new ChannelData();
                Payout.GetDataByChannel(Int32.Parse(chkbox.Name), ref d);

                if (chkbox.Checked)
                    Payout.ChangeNoteRoute(d.Value, d.Currency, false, textBox1);
                else
                    Payout.ChangeNoteRoute(d.Value, d.Currency, true, textBox1);

                // Ensure payout ability is enabled in the validator
                Payout.EnablePayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        private void btnResetHopper_Click(object sender, EventArgs e)
        {
            Hopper.Reset(textBox1);
            // Force reconnect by closing com port
            //CommsLibrary.CloseComPort();
        }

        private void btnResetPayout_Click(object sender, EventArgs e)
        {
            Payout.Reset(textBox1);
            // Force reconnect by closing com port
            //CommsLibrary.CloseComPort();
        }

        private void setLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = new frmSetLevel(Hopper);
            f.Show();
        }

        private void setAllToZeroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < Hopper.NumberOfChannels + 1; i++)
                Hopper.SetCoinLevelsByChannel(i, 0, textBox1);
            Hopper.UpdateData();
        }

        private void reconnectionTimer_Tick(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.Timer)
            {
                System.Windows.Forms.Timer t = sender as System.Windows.Forms.Timer;
                t.Enabled = false;
            }
        }

        private void btnFloat_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate
                if (tbFloatAmount.Text == "" || tbMinPayout.Text == "" || tbFloatCurrency.Text == "")
                    return;

                // Parse to a float
                float fFa = float.Parse(tbFloatAmount.Text);
                float fMp = float.Parse(tbMinPayout.Text);

                int fa = (Int32)(fFa * 100); // multiply by 100 for penny value
                // If payout selected
                if (cbFloatSelect.Text == "SMART Payout")
                {
                    int mp = (int)(fMp * 100); // multiply by 100 for penny value
                    Payout.SetFloat(mp, fa, tbFloatCurrency.Text.ToCharArray(), textBox1);
                }
                // Or if Hopper
                else if (cbFloatSelect.Text == "SMART Hopper")
                {
                    short mp = (short)(fMp * 100); // multiply by 100 for penny value
                    Hopper.SetFloat(mp, fa, tbFloatCurrency.Text.ToCharArray(), textBox1);
                }
                else
                    MessageBox.Show("Choose a device to float from!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        private void btnPayoutByDenom_Click(object sender, EventArgs e)
        {
            if (hopperRunning && payoutRunning && ((payoutByDenomFrm == null) || (payoutByDenomFrm != null && !payoutByDenomFrm.Visible)))
            {
                payoutByDenomFrm = new frmPayoutByDenom(Payout, Hopper, textBox1);
                payoutByDenomFrm.Show();
            }
        }

        */
    }
}
