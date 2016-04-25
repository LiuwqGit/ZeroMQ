﻿using System;
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
            string[] weathers = new string[5] { "晴朗", "多云", "阴天", "小雨", "暴雪" };

            Console.CancelKeyPress += Console_CancelKeyPress;

            Console.WriteLine("发布多个地区天气预报：");

            using (NetMQContext context = NetMQContext.Create())
            {
                using (var publisher = context.CreatePublisherSocket())
                {
                    publisher.Bind("tcp://127.0.0.1:5556");

                    var rng = new Random();
                    string msg;
                    int sleeptime = 10;

                    while (_terminateEvent.WaitOne(0) == false)
                    {
                        //随机生成天气数据
                        int zipcode = rng.Next(0, 99);
                        int temperature = rng.Next(-50, 50);
                        int weatherId = rng.Next(0, 4);

                        msg = string.Format("{0} {1} {2}", zipcode, temperature, weathers[weatherId]);
                        //publisher.Send(msg, Encoding.UTF8, SendReceiveOptions.DontWait);
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
