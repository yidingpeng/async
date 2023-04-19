using System;
using Microsoft.Extensions.DependencyInjection;
using RW.Position.Extensions;

namespace RW.Position
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var servicesPrvider = ConfigurationService.Injection();
            var myService = servicesPrvider.GetService<OnDemandSubscription>();
            Console.WriteLine("Hello");
            Console.ReadKey();
        }
    }
}
