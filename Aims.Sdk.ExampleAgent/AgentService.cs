using System;
using System.Diagnostics;
using System.ServiceProcess;

namespace Aims.Sdk.ExampleAgent
{
    public class AgentService : ServiceBase
    {
        private readonly EventLog _eventLog;
        private readonly object _lock = new object();
        private Agent _agent;

        public AgentService()
        {
            InitializeComponent();

            _eventLog = new EventLog(AgentConstants.Service.Log) { Source = AgentConstants.Service.EventSource };
        }

        internal void Start()
        {
            OnStart(new string[0]);
        }

        protected override void OnStart(string[] args)
        {
            lock (_lock)
            {
                if (_agent != null) return;

                try
                {
                    var apiAddress = new Uri(new Uri(Config.ApiEndPoint), "environments/" + Config.EnvironmentId);
                    _agent = new Agent(apiAddress, Config.Token);
                }
                catch (Exception ex)
                {
                    _eventLog.WriteEntry(String.Format("Failed to start the agent:{0}{1}", Environment.NewLine, ex),
                        EventLogEntryType.Error);
                }
            }
        }

        protected override void OnStop()
        {
            lock (_lock)
            {
                if (_agent == null) return;

                try
                {
                    _agent.Dispose();
                }
                catch (Exception ex)
                {
                    _eventLog.WriteEntry(String.Format("Failed to stop the agent:{0}{1}", Environment.NewLine, ex),
                        EventLogEntryType.Error);
                }
                _agent = null;
            }
        }

        private void InitializeComponent()
        {
            ServiceName = AgentConstants.Service.ApplicationName;
        }
    }
}