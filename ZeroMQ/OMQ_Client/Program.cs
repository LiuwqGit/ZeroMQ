using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetMQ;

namespace OMQ_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (NetMQContext context = NetMQContext.Create())
            {
                Client(context);
            }
        }

        private static void Client(NetMQContext context)
        {
            using (NetMQSocket clientSocket = context.CreateRequestSocket())
            {
                Random rd = new Random();
                int num = rd.Next(0, 100);
                clientSocket.Connect("tcp://127.0.0.1:5555");
                while (true)
                {
                    Console.WriteLine(num + ",Please enter your message:");
                    string message = Console.ReadLine();
                    clientSocket.SendFrame(num + ":" + message);

                    string answer = clientSocket.ReceiveFrameString();

                    Console.WriteLine("Answer from server:{0}", answer);

                    if (message == "exit")
                    {
                        break;
                    }
                }
            }

        }
    }
}
