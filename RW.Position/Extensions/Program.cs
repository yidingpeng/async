using System;
using Microsoft.Extensions.DependencyInjection;
using RW.Position.Extensions;
using RW.Position.websocketServers;

namespace RW.Position
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var servicesPrvider = ConfigurationService.Injection();
            var myService = servicesPrvider.GetService<OnDemandSubscription>();
            servicesPrvider.GetService<WebSocketServerConfig>();
            
            Console.ReadKey();
        }
    }
}
