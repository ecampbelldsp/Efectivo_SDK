using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSSP_example.Pipeline
{
    public class Connect_BaseHopper
    {
        public bool hopperConnecting = false;
        public bool hopperRunning = false;
        public int successfulConnectingAttempt = -1;

        public void run(BaseHopper Hopper)
        {
            int attempts = 10000;
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
                    successfulConnectingAttempt = i;
                    Hopper.CommandStructure.EncryptionStatus = true; // now encrypting
                    // find the max protocol version this validator supports
                    byte maxPVersion = FindMaxHopperProtocolVersion(Hopper);
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

            }
        }

        private byte FindMaxHopperProtocolVersion(BaseHopper Hopper)
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

        private bool IsHopperSupported(char type)
        {
            if (type == (char)0x03 || type == (char)0x09)
                return true;
            return false;
        }
    }
}
