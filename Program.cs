using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITLlib;
using eSSP_example.Pipeline;
using System.Threading;
using System.IO;

namespace eSSP_example
{
    internal class Program
    {
        static int Main(string[] args)
        {
            Log.updatePago("0");
            //float amountToPay = float.Parse(args[0]);
            BaseHopper Hopper_test = new BaseHopper();
            BasePayout Payout_test = new BasePayout();
            BaseForm1 form = new BaseForm1(Hopper_test, Payout_test);

            //Console.WriteLine("Primera linea " + args.Length);
           // Console.WriteLine("Segunda linea ");
            bool cobrar = true;

            Response response = new Response();

            float amountToPay = float.Parse(args[1]);
            //Iniciando flujo para cobrar
            
            Console.WriteLine("Cantidad a pagar :" + amountToPay + " E\n");
            Log.write("Cantidad a pagar :" + amountToPay + " E\n");
            response = form.CountingPayment(amountToPay);

            float cashBack = response.cashBack;

            if (!response.success)
            {
                Console.WriteLine(response.message);
                Log.write("off");
                return -1;
            }
                //Comprobando si hay que devolver dinero
            if(cashBack > 0)
            {
                //Thread.Sleep(5000);
                Console.WriteLine("Despensing cashback amout: " + cashBack + " E");
                Log.write("Esperar cashback");
                response = form.Pagar(cashBack.ToString());
                    
                //Comprobando que la devolucion fue exitosa
                if (!response.success)
                {
                    Log.write("devolver");
                    Console.WriteLine("devolver");
                    cashBack = cashBack + amountToPay;
                    response = form.Pagar(cashBack.ToString());
                    Console.WriteLine("El sistema no tiene dinero para Cashback. Le devolvemos el dinero ingresado");
                    
                    return -1;
                }
                else
                {
                    Console.WriteLine("Payment successful");
                    Log.write("done");
                    return 0;
                }
            }

            Log.write("done");
            Console.ReadLine();

            return 0;
        }
    }
}
