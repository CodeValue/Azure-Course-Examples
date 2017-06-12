using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Contract;
using Microsoft.ServiceBus;

namespace AzureRelayDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost sh = new ServiceHost(typeof(ProblemSolver));

            sh.AddServiceEndpoint(
               typeof(IProblemSolver), new NetTcpBinding(),
               "net.tcp://localhost:9358/solver");

            sh.AddServiceEndpoint(
               typeof(IProblemSolver), new NetTcpRelayBinding(),
               ServiceBusEnvironment.CreateServiceUri("sb", "relay2", "solver"))
                .Behaviors.Add(new TransportClientEndpointBehavior
                {
                    TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider("RootManageSharedAccessKey", "F/czd6GV4N6okqJqUScqPsiNzT0XPs+cvZmu1k6C3bk=")
                });

            sh.Open();

            Console.WriteLine("Press ENTER to close");
            Console.ReadLine();

            sh.Close();
        }

    }
}
