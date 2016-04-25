using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

<<<<<<< HEAD
using NetMQ;

=======
>>>>>>> 43662d24bcc9a4060b6bcbbb8b2a352410ef20c5
namespace OMQ_Client
{
    class Program
    {
        static void Main(string[] args)
        {
<<<<<<< HEAD
            using (NetMQContext context = NetMQContext.Create())
            {
                Client(context);
            }
        }

        private static void Client(NetMQContext context)
        {
            using (NetMQSocket clientSocket = context.CreateRequestSocket())
            {
                clientSocket.Connect("tcp://127.0.0.1:5555");
                while (true)
                {
                    Console.WriteLine("Please enter your message:");
                    string message = Console.ReadLine();
                    clientSocket.SendFrame(message);

                    string answer = clientSocket.ReceiveFrameString();

                    Console.WriteLine("Answer from server:{0}", answer);

                    if (message == "exit")
                    {
                        break;
                    }
                }
            }
=======
>>>>>>> 43662d24bcc9a4060b6bcbbb8b2a352410ef20c5
        }
    }
}
