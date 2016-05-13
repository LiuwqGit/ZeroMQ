using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetMQ;

namespace OMQ_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            using (NetMQContext context = NetMQContext.Create())
            {
                Server(context);
            }
        }

        private static void Server(NetMQContext context)
        {
            using (NetMQSocket serverSocket = context.CreateResponseSocket())
            {
                serverSocket.Bind("tcp://127.0.0.1:5555");
                while (true)
                {
                    string message1 = serverSocket.ReceiveFrameString();

                    Console.WriteLine("Receive message :\r\n{0}\r\n", message1);

                    string[] msg = message1.Split(':');
                    string message = msg[1];


                    #region 根据接收到的消息，返回不同的信息
                    if (message == "Hello")
                    {
                        serverSocket.SendFrame("World");
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

        }
    }
}
