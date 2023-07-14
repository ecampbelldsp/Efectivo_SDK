﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITLlib;
using eSSP_example.Pipeline;
using System.Threading;

namespace eSSP_example
{
    internal class Program
    {
        static void Main(string[] args)
        {

            BaseHopper Hopper_test = new BaseHopper();
            BasePayout Payout_test = new BasePayout();
            BaseForm1 form = new BaseForm1(Hopper_test, Payout_test);

            bool cobrar = false;

            Response response = new Response();

            if (cobrar)
            {
                //Iniciando flujo para cobrar
                float amountToPay = 0.0002F;
                Console.WriteLine("Cantidad a pagar :" + amountToPay + " E\n");
                float cashBack = form.CountingPayment(amountToPay);

                //Comprobando si hay que devolver dinero
                if (cashBack > 0)
                {
                    Thread.Sleep(5000);
                    Console.WriteLine("Despensing cashback amout: " + cashBack + " €");
                    response = form.Pagar(cashBack.ToString());
                    
                    //Comprobando que la devolucion fue exitosa
                    if (!response.success)
                    {
                        cashBack = cashBack + amountToPay;
                        response = form.Pagar(cashBack.ToString());
                        Console.WriteLine("El sistema no tiene dinero para Cashback. Le devolvemos el dinero ingresado");
                    }
                }
             }
             else
             { 
                response = form.Pagar("0.50");/**/
                if(!response.success)
                {
                    Console.WriteLine("Unable to pay amount");
                }
                else
                {
                    Console.WriteLine("Successful payment");
                }
                
             }
            Console.ReadLine();



            //string a = "a";

            /*

            Connect_BaseHopper connect_Hooper = new Connect_BaseHopper();
            Connect_BasePayout connect_Payout = new Connect_BasePayout();

            connect_Hooper.run(Hopper_test);
            connect_Payout.run(Payout_test);

            Payout_test.DoPoll();
            Hopper_test.DoPoll();

            //bool test = false;
            char[] currency = { 'E', 'U', 'R' };
            string amount = "0.2";

            Payment Payment_test = new Payment(Hopper_test, Payout_test);

            Payment_test.CalculatePayout(amount, currency);

            Form1 Form = new Form1();
            bool flag = Form.Start();
           
            Console.WriteLine("Hello World");
            Console.ReadLine(); */
        }
    }
}