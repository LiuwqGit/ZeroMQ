using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

<<<<<<< HEAD
using NetMQ;

=======
>>>>>>> 43662d24bcc9a4060b6bcbbb8b2a352410ef20c5
namespace OMQ_Server
{
    class Program
    {
        static void Main(string[] args)
        {
<<<<<<< HEAD
            using (NetMQContext context = NetMQContext.Create())
            {
                Server(context);
            }
        }

        private static void Server(NetMQContext context)
        {
            using (NetMQSocket serverSocket = context.CreateResponseSocket())
            {
                serverSocket.Bind("tcp://*:5555");
                while (true)
                {
                    string message = serverSocket.ReceiveFrameString();

                    Console.WriteLine("Receive message {0}", message);

                    #region 根据接收到的消息，返回不同的信息
                    if (message == "Hello1")
                    {
                        serverSocket.SendFrame("World1");
                    }
                    else if (message == "ni hao ")
                    {
                        serverSocket.SendFrame("你好！");
                    }
                    else if (message == "hi")
                    {
                        serverSocket.SendFrame("HI");
                    }
                    else
                    {
                        serverSocket.SendFrame(message);
                    }
                    #endregion



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
