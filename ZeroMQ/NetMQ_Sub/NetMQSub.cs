using NetMQ;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMQ_Sub
{
    public class NetMQSub
    {
        public delegate void GetDataHandler(string message);
        public static event GetDataHandler OnGetData;
        /// <summary>
        /// NetMQ 订阅端
        /// </summary>
        public static void Start()
        {
            var rng = new Random();
            int zipcode = rng.Next(0, 99);
            Console.WriteLine("接收本地天气预报{0}……", zipcode);

            OnGetData += new GetDataHandler(ProcessData);

            using (var context = NetMQContext.Create())
            {
                using (var subscriber = context.CreateSubscriberSocket())
                {
                    subscriber.Connect("tcp://127.0.0.1:5556");
                    subscriber.Subscribe(zipcode.ToString(CultureInfo.InvariantCulture));
                    //subscriber.Subscribe("");

                    while (true)
                    {
                        string results = subscriber.ReceiveFrameString(Encoding.UTF8);
                        Console.WriteLine(".");

                        string[] split = results.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        int zip = int.Parse(split[0]);
                        if (zip == zipcode)
                        {
                            OnGetData(results);
                        }
                    }

                }
            }
        }

        private static void ProcessData(string message)
        {
            Console.WriteLine("天气情况：" + message);
        }


    }
}
