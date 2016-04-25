using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMQ_Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            //Task Worker
            //Connects PULL socket to tcp://localhost:5557
            //collects workload for socket from Ventilator via that socket
            //collects PUSH socket to tcp://localhost:5558
            //Sends results to Sink via that socket

            Console.WriteLine("=====WORKER=====");

            ////Task 方式多线程
            //foreach (Worker client in Enumerable.Range(0, 1000).Select(x => new Worker()))
            //{
            //    client.Run();
            //}

            //多核计算方式多线程
            var actList = Enumerable.Range(0, 50).Select(x => new Worker()).Select(client => (Action)(client.Run)).ToList();
            var paraOption = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };
            Parallel.Invoke(paraOption, actList.ToArray());

            Console.ReadLine();
        }
    }
}
