using System;
using System.Collections.Generic;
using Env = System.Environment;

namespace Aims.Sdk.ExampleAgent
{
    public class TopologyMonitor : MonitorBase<Node>
    {
        private readonly EnvironmentApi _api;
        private readonly NodeRef _nodeRef;

        public TopologyMonitor(EnvironmentApi api, NodeRef nodeRef)
            : base((int)TimeSpan.FromMinutes(5).TotalMilliseconds)
        {
            _api = api;
            _nodeRef = nodeRef;
        }

        protected override Node[] Collect()
        {
            return new[]
            {
                new Node
                {
                    NodeRef = _nodeRef,
                    Name = Env.MachineName,
                    ModificationTime = DateTimeOffset.Now,
                    Status = GetStatus(),
                    Properties = new Dictionary<string, string>
                    {
                        { AgentConstants.PropertyType.ServerOs, Env.OSVersion.VersionString },
                    },
                },
            };
        }

        protected override void Send(Node[] items)
        {
            _api.Nodes.Send(items);
        }

        private static string GetStatus()
        {
            // The system is unavailable from 00:00 to 00:59.
            return DateTimeOffset.Now.Hour == 0 ? AgentConstants.Status.Unavailable : AgentConstants.Status.Started;
        }
    }
}