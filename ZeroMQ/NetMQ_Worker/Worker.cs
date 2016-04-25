using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetMQ;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetMQ_Worker
{
    sealed class Worker
    {
        public void Run()
        {
            Task.Run(() =>
            {
                using (NetMQContext context = NetMQContext.Create())
                {
                    //socket to receive messages on
                    using (var receiver = context.CreatePullSocket())
                    {
                        receiver.Connect("tcp://127.0.0.1:5557");

                        //socket to send messages on
                        using (var sender = context.CreatePushSocket())
                        {
                            sender.Connect("tcp://127.0.0.1:5558");

                            //process tasks forever
                            while (true)
                            {
                                //workload from the vetilator is a simple delay
                                //to simulate some work being done, see
                                //Ventilator.csproj Program.cs for the workload sent
                                //In real life some more meaningful work would be done

                                string workload = receiver.ReceiveString();

                                var person = JsonConvert.DeserializeObject<Person>(workload);

                                //JArray ja = JArray.Parse(workload);

                                Console.WriteLine("Person {Id:" + person.Id + ",Name:" + person.Name + ",BirthDay:" +
                                                     person.BirthDay + ",Address:{Line1:" + person.Address.Line1 +
                                                     ",Line2:" + person.Address.Line2 + "}}");
                                Console.WriteLine("Sending to Sink:" + person.Id);
                                sender.SendFrame(person.Id + "");
                                //sender.SendFrame("liuwq" + DateTime.Now);
                            }

                            //simulate some work being done
                            //Thread.Sleep(int.Parse(workload));
                        }
                    }
                }
            });
        }
    }
}
