using System;
using RW.Position.Extensions;

namespace RW.Position
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConfigurationService.Injection();
            new OnDemandSubscription();
            Console.WriteLine("Hello");
            Console.ReadKey();
        }
    }
}
