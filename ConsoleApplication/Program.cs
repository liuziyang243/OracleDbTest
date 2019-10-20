using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using OracleDbTest.service;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            ServiceHost host = new ServiceHost(typeof(PersonService));
            host.AddServiceEndpoint(typeof(IPersionService), new WSHttpBinding(), "http://localhost:8733/Design_Time_Addresses/OracleDbTest.service.PersonService/");
            if (host.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
            {
                ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                behavior.HttpGetEnabled = true;
                behavior.HttpGetUrl = new Uri("http://localhost:8733/Design_Time_Addresses/OracleDbTest.service.PersonService/wsdl");
                host.Description.Behaviors.Add(behavior);
            }

            host.Open();

            Console.WriteLine("CalculaorService已经启动，按任意键终止服务！");

            Console.Read();
            host.Close();
        }
    }
}
