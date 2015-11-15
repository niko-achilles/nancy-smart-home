// ****************************************************************************
// <author>Nikolaos Kokkinos</author>
// <email>nik.kokkinos@windowslive.com</email>
// <date>28.01.2015</date>
// <project>SmartHomeNancySelfHost</project>
// <web>http://nikolaoskokkinos.wordpress.com/</web>
// ****************************************************************************


using Nancy.Hosting.Self;
using SmartHomeNancy;
using System;

namespace SmartHomeNancySelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            DatapointsModule artificiaReference;
            HelloModule artificialHomeModule;

            string host = args[0];
            string port = args[1];

            var nancyHost = new NancyHost(new
            Uri("http://"+host+":"+port+"/"));

            nancyHost.Start();

            
            Console.ReadKey();
            nancyHost.Stop();
        }
    }
}
