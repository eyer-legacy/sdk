using System;
using System.Collections.Generic;
using Env = System.Environment;

namespace Aims.Sdk.ExampleAgent
{
    public class Agent : IDisposable
    {
        private readonly TopologyMonitor _topologyMonitor;
        private readonly StatisticsMonitor _statisticsMonitor;

        public Agent(Uri apiAddress, Guid environmentId, string token)
        {
            var api = new Api(apiAddress, token) { EnvironmentId = environmentId };
            var nodeRef = new NodeRef
            {
                NodeType = AgentConstants.NodeType.Server,
                Parts = new Dictionary<string, string> { { "machine-name", Env.MachineName } },
            };

            _statisticsMonitor = new StatisticsMonitor(api, nodeRef);
            _topologyMonitor = new TopologyMonitor(api, nodeRef);
        }

        public void Dispose()
        {
            _statisticsMonitor.Dispose();
            _topologyMonitor.Dispose();
        }
    }
}