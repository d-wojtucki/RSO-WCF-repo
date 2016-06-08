using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Czytelnia
{
    class Program
    {
        static void Main(string[] args)
        {
            using(ServiceHost host = new ServiceHost(typeof(WCFCzytelniaSerwis)))
            {
                try {
                    host.Open();
                    Console.WriteLine("Server open!");
                    Console.WriteLine("<Press enter to close server>");
                    Console.ReadLine();
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
        }
    }
}
