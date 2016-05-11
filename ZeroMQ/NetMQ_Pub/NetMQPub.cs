using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using NetMQ;

namespace NetMQ_Pub
{
    public class NetMQPub
    {
        readonly static ManualResetEvent _terminateEvent = new ManualResetEvent(false);
        /// <summary>
        /// NetMQ 发布端
        /// </summary>
        public static void Start()
        {
            string[] weathers = new string[6] { "晴朗", "多云", "阴天", "霾", "雨", "雪" };
             
            Console.WriteLine("发布多个地区天气预报：");

            using (NetMQContext context = NetMQContext.Create())
            {
                using (var publisher = context.CreatePublisherSocket())
                {
                    publisher.Bind("tcp://127.0.0.1:5556");

                    var rng = new Random();
                    string msg;
                    int sleeptime = 1000;//1秒

                    ///指定发布的时间间隔，1秒
                    while (_terminateEvent.WaitOne(1000) == false)
                    {
                        //随机生成天气数据
                        int zipcode = rng.Next(0, 99);
                        int temperature = rng.Next(-50, 50);
                        int weatherId = rng.Next(0, 5);

                        msg = string.Format("{0} {1} {2}", zipcode, temperature, weathers[weatherId]);
                        publisher.SendFrame(msg);

                        Console.WriteLine(msg);
                        Thread.Sleep(sleeptime);
                    }
                }
            }
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("exit……");
            _terminateEvent.Set();
        }
    }
}
