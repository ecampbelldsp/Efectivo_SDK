using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eSSP_example;
using eSSP_example.Pipeline;


namespace eSSP_example.Pipeline
{
    public class Connect
    {
        public bool payoutConnecting = true;
        public void init(BasePayout Payout)
        {
            Payout.CommandStructure.ComPort = Global.ValidatorComPort;
            Payout.CommandStructure.SSPAddress = Global.Validator1SSPAddress;
            Payout.CommandStructure.BaudRate = 9600;
            Payout.CommandStructure.Timeout = 1000;
            Payout.CommandStructure.RetryLevel = 3;
            int attempts = 10;

            for (int i = 0; i < attempts; i++)
            { //Step 1
                // turn encryption off for first stage
                Payout.CommandStructure.EncryptionStatus = false;

                // Open port first, if the key negotiation is successful then set the rest up
                if (Payout.OpenPort() && Payout.NegotiateKeys())
                {
                    Payout.CommandStructure.EncryptionStatus = true; // now encrypting
                    // find the max protocol version this validator supports
                    byte maxPVersion = FindMaxPayoutProtocolVersion(Payout);
                    if (maxPVersion >= 6)
                        Payout.SetProtocolVersion(maxPVersion);
                    else
                    {
                       // MessageBox.Show("This program does not support units under protocol 6!", "ERROR");
                        payoutConnecting = false;
                        return;
                    }
                    // get info from the validator and store useful vars
                    Payout.PayoutSetupRequest(log);
                    // check the right unit is connected
                    if (!IsValidatorSupported(Payout.UnitType))
                    {
                        MessageBox.Show("Unsupported type shown by SMART Payout, this SDK supports the SMART Payout and the SMART Hopper only");
                        payoutConnecting = false;
                        Application.Exit();
                        return;
                    }
                    // inhibits, this sets which channels can receive notes
                    Payout.SetInhibits(log);
                    // Get serial number.
                    Payout.GetSerialNumber(log);
                    // enable payout
                    Payout.EnablePayout(log);
                    // set running to true so the validator begins getting polled
                    payoutRunning = true;
                    payoutConnecting = false;

                    return;
                }
                // reset timer
                reconnectionTimer.Enabled = true;
                while (reconnectionTimer.Enabled)
                {
                    if (CHelpers.Shutdown)
                    {
                        payoutConnecting = false;
                        return;
                    }

                    Application.DoEvents();
                    Thread.Sleep(1);
                }
            }
            payoutConnecting = false;

        }

        private byte FindMaxPayoutProtocolVersion(BasePayout Payout)
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
    }
