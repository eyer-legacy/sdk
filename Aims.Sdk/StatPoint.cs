using System;

namespace Aims.Sdk
{
    public class StatPoint
    {
        public NodeRef NodeRef { get; set; }

        public string StatType { get; set; }

        public DateTimeOffset Time { get; set; }

        public double Value { get; set; }
    }
}