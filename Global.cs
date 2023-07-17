using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSSP_example
{
    public class Global
    {
        public static string ValidatorComPort = "COM4";
        public static byte Validator1SSPAddress = 0;
        public static byte Validator2SSPAddress = 16;
        public static float NoteCountingPayment = 0;
        public static float CoinCountingPayment = 0;
        public static bool NotePaymentActive = true;  //Only for note. 
        public static bool CoinPaymentActive = true;  //Only for coin. 

        public static bool NotAllowedNote = true;
        public static int NotAllowedNoteValue = 0;


    }
}
