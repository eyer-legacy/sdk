using System;
using System.Collections.Generic;

namespace Aims.Sdk.ExampleAgent
{
    public class Agent : IDisposable
    {
        private readonly TopologyMonitor _topologyMonitor;
        private readonly StatisticsMonitor _statisticsMonitor;

        public Agent(Uri apiAddress, string token, long systemId)
        {
            var api = new Api(apiAddress, token, systemId);
            var nodeRef = new NodeRef
            {
                NodeType = AgentConstants.NodeType.Server,
                Parts = new Dictionary<string, string> { { "machine-name", Environment.MachineName } },
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