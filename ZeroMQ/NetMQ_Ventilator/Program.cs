using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetMQ;

namespace NetMQ_Ventilator
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Task Ventilator
            //Binds PUSH socket to tcp://localhost:5557
            //Sends batch of tasks to workers via that socket
            Console.WriteLine("=========VENTILATOR===========");

            Console.WriteLine("Press enter when worker are ready");
            Console.ReadLine();

            //the first message it "0" and signals start of batch
            //see the Sink.csproj Program.cs file for where this is used
            Console.WriteLine("Sending start of batch to Sink");

            var ventilator = new Ventilator();
            ventilator.Run();

            Console.WriteLine("Press Enter to quit");
            Console.ReadLine();
        }
    }
}
