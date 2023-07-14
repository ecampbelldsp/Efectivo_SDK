using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSSP_example.Pipeline
{
    public class Payment
    {
        BaseHopper Hopper;
        BasePayout Payout;
        public Payment(BaseHopper Hopper_in, BasePayout Payout_in)
            {
                Hopper = Hopper_in;
                Payout = Payout_in;
            }
        public void CalculatePayout(string amount, char[] currency)
        {
           /* if(!Payout.OpenPort() && !Payout.NegotiateKeys() & !Hopper.OpenPort()  && !Hopper.NegotiateKeys())
            {
                return;
            }*/
            float payoutAmount;
            try
            {
                // Parse it to a number
                payoutAmount = float.Parse(amount) * 100;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return;
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
                        "Error with Payout", MessageBoxButtons.YesNo);*/

                    //if (res == System.Windows.Forms.DialogResult.No)
                        return;
                    //else
                     //   payoutAmount += payoutList;
                }

                // SP is ok to pay
                Payout.PayoutAmount(payoutList, currency, false);
            }

            // Now if there is any left over, request from Hopper
            if (payoutAmount > 0)
            {
                // Test Hopper first
                Hopper.PayoutAmount((int)payoutAmount, currency, true);
                if (Hopper.CommandStructure.ResponseData[0] != 0xF0)
                {
                   // MessageBox.Show("Unable to pay requested amount!");
                    return;
                }

                // Hopper is ok to pay
                Hopper.PayoutAmount((int)payoutAmount, currency, false);
            }
        }


    }
}
