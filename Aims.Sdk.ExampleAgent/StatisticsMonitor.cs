using System;
using System.Diagnostics;

namespace Aims.Sdk.ExampleAgent
{
    public class StatisticsMonitor : MonitorBase<StatPoint>
    {
        private const string CategoryName1 = "Processor Information";
        private const string CategoryName2 = "Processor";
        private const string CounterName = "% Processor Time";
        private const string InstanceName = "_Total";

        private readonly EnvironmentApi _api;
        private readonly NodeRef _nodeRef;

        private PerformanceCounter _counter;

        public StatisticsMonitor(EnvironmentApi api, NodeRef nodeRef)
            : base((int)TimeSpan.FromMinutes(1).TotalMilliseconds)
        {
            _api = api;
            _nodeRef = nodeRef;
        }

        private PerformanceCounter Counter
        {
            get
            {
                try
                {
                    // There are two different categories for CPU that are available in different Windows versions,
                    // and this is the easiest way to find out which one is available here.
                    return _counter = _counter ?? new PerformanceCounter(CategoryName1, CounterName, InstanceName);
                }
                catch (InvalidOperationException)
                {
                    return _counter = _counter ?? new PerformanceCounter(CategoryName2, CounterName, InstanceName);
                }
            }
        }

        public override void Dispose()
        {
            PerformanceCounter counter = _counter;
            if (counter != null)
            {
                counter.Close();
            }
            base.Dispose();
        }

        protected override StatPoint[] Collect()
        {
            float value;
            return TryGetValue(out value)
                ? new[]
                {
                    new StatPoint
                    {
                        NodeRef = _nodeRef,
                        StatType = AgentConstants.StatType.ServerCpu,
                        Time = DateTimeOffset.Now,
                        Value = value,
                    }
                }
                : new StatPoint[0];
        }

        protected override void Send(StatPoint[] items)
        {
            _api.StatPoints.Send(items);
        }

        private bool TryGetValue(out float value)
        {
            PerformanceCounter counter = null;
            try
            {
                counter = Counter;
                value = counter.NextValue();
                return true;
            }
            catch (InvalidOperationException)
            {
                value = Single.NaN;
                if (counter != null)
                {
                    counter.Close();
                }
                return false;
            }
        }
    }
}