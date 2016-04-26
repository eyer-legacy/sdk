using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;

namespace Aims.Sdk.ExampleAgent
{
    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();

            if (EventLog.SourceExists(AgentConstants.Service.EventSource))
            {
                EventLog.DeleteEventSource(AgentConstants.Service.EventSource);
            }

            ConfigureEventLogInstaller(AgentConstants.Service.Log, AgentConstants.Service.EventSource);
        }

        private static T FindInstaller<T>(InstallerCollection installers)
            where T : Installer
        {
            return installers.Cast<Installer>()
                .Select(i => i as T ?? FindInstaller<T>(i.Installers))
                .FirstOrDefault(i => i != null);
        }

        private void ConfigureEventLogInstaller(string log, string source)
        {
            var eventLogInstaller = FindInstaller<EventLogInstaller>(Installers);
            if (eventLogInstaller == null)
            {
                eventLogInstaller = new EventLogInstaller();
                Installers.Add(eventLogInstaller);
            }

            eventLogInstaller.Source = source;
            eventLogInstaller.Log = log;
        }

        private void InitializeComponent()
        {
            Installers.AddRange(new Installer[]
            {
                new ServiceProcessInstaller
                {
                    Account = ServiceAccount.LocalSystem,
                    Password = null,
                    Username = null,
                },
                new ServiceInstaller
                {
                    Description = "Example of AIMS .NET SDK usage.",
                    DisplayName = AgentConstants.Service.ApplicationName,
                    ServiceName = AgentConstants.Service.ServiceName,
                    StartType = ServiceStartMode.Automatic,
                },
            });
        }
    }
}