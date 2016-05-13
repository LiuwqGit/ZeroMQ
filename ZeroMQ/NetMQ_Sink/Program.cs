using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetMQ;
using System.Diagnostics;

namespace NetMQ_Sink
{
    class Program
    {
        static void Main(string[] args)
        {
            // Task Sink
            //Bind PULL socket to tcp://127.0.0.1:5558
            //Collects results from workers via that socket
            Console.WriteLine("=====SINK=====");

            using (NetMQContext context = NetMQContext.Create())
            {
                //socket to receive messages on
                using (var receiver = context.CreatePullSocket())
                {
                    receiver.Bind("tcp://localhost:5558");

                    //wait for start of batch (see Ventilator.csproj  Program.cs)
                    var startOfBatchTrigger = receiver.ReceiveFrameString();
                    Console.WriteLine("Seen start of batch");

                    //Start our clock now
                    Stopwatch watch = new Stopwatch();
                    watch.Start();

                    for (int taskNumber = 0; taskNumber < 10000; taskNumber++)
                    {
                        var workerDoneTrigger = receiver.ReceiveFrameString();
                        Console.WriteLine(workerDoneTrigger);
                    }

                    watch.Stop();

                    Console.WriteLine();
                    Console.WriteLine("Total elapsed time {0} msec", watch.ElapsedMilliseconds);
                    Console.ReadLine();
                }

            }
        }
    }
}
