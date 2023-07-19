using System;
using System.IO;

namespace eSSP_example
{
    public class Log
    {

        public static void write(string message)
        {
            using (StreamWriter writer = new StreamWriter("logs/log.txt", false))
            {
                // Write content to the file
                writer.WriteLine(message);

            }

        }
    }
}