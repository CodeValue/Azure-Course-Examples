using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Contract;
using Microsoft.ServiceBus;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press ENTER to make a call to the service");
            Console.ReadLine();

            var cf = new ChannelFactory<IProblemSolverChannel>(
                new NetTcpRelayBinding(),
                new EndpointAddress(ServiceBusEnvironment.CreateServiceUri("sb", "relay2", "solver")));

            cf.Endpoint.Behaviors.Add(new TransportClientEndpointBehavior
            {
                TokenProvider =
                    TokenProvider.CreateSharedAccessSignatureTokenProvider(
                        "RootManageSharedAccessKey",
                        "F/czd6GV4N6okqJqUScqPsiNzT0XPs+cvZmu1k6C3bk=")
            });
            string op = "";
            using (var ch = cf.CreateChannel())
            {
                while (op.ToLower()!="x")
                {
                    Console.WriteLine(ch.AddNumbers(4, 5));
                    Console.WriteLine("Press ENTER to make another call, or x to exit");
                    op=Console.ReadLine();
                }
            }


            
        }
    }
}
