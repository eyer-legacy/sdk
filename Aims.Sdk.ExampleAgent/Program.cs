using System;
using System.Collections;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;

namespace Aims.Sdk.ExampleAgent
{
    internal static class Program
    {
        private static void InstallService()
        {
            using (var installer = new ProjectInstaller())
            {
                installer.Context = new InstallContext("",
                    new[] { String.Format("/assemblypath={0}", Assembly.GetExecutingAssembly().Location) });

                installer.Install(new Hashtable());
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main(string[] args)
        {
            if (args.Length >= 1 && args[0] == "/install")
            {
                InstallService();
                return;
            }

            var service = new AgentService();
            if (args.Contains("/console"))
            {
                service.Start();
                Thread.Sleep(-1);
            }
            else
            {
                ServiceBase.Run(service);
            }
        }
    }
}