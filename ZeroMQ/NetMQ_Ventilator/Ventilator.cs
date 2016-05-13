using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetMQ;
using Model;
using Newtonsoft.Json;
using System.IO;

namespace NetMQ_Ventilator
{
    sealed class Ventilator
    {
        public void Run()
        {
            Task.Run(() =>
            {
                using (var context = NetMQContext.Create())
                using (var sender = context.CreatePushSocket())
                using (var sink = context.CreatePushSocket())
                {
                    sender.Bind("tcp://*:5557");
                    sink.Connect("tcp://127.0.0.1:5558");
                    sink.SendFrame("0");

                    Console.WriteLine("Sending tasks to workers");
                    //RuntimeTypeModel.Default.MetadataTimeoutMilliseconds = 300000;

                    //send 100 tasks(workload for tasks,is just some random sleep time that
                    //the workers can perofrm, in real life each work would do more than sleep

                    for (int taskNumber = 0; taskNumber < 10000; taskNumber++)
                    {
                        Console.WriteLine("Workload:{0}", taskNumber);
                        var person = new Person
                        {
                            Id = taskNumber,
                            Name = "First",
                            BirthDay = DateTime.Parse("1981-11-15"),
                            Address = new Address { Line1 = "line1", Line2 = "line2" }
                        };

                        using (var sm = new MemoryStream())
                        {
                            string s = JsonConvert.SerializeObject(person);

                            sender.SendFrame(s);
                        }
                    }
                }
            });
        }
    }
}
