using System;
using System.IO;

namespace eSSP_example
{
    public class Log
    {
        /*public static bool connectionOn = false;
        public static bool paymentSuccesful = false;
        public static bool NeedcashBack = false;
        public static bool cashBackAvailable = false;*/

        public static void write(string message)
        {
            using (StreamWriter writer = new StreamWriter("Logs/log.txt", false))
            {
                // Write content to the file
                writer.WriteLine(message);

            }

        }


        public static void updatePago(string message)
        {
            using (StreamWriter writer = new StreamWriter("Logs/pagado.txt", false))
            {
                // Write content to the file
                writer.WriteLine(message);

            }

        }


    }
}