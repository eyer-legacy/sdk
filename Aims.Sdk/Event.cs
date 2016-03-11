using System;
using System.Collections.Generic;

namespace Aims.Sdk
{
    public class Event
    {
        public Dictionary<string, string> Data { get; set; }

        public DateTimeOffset? EndTime { get; set; }

        public string EventType { get; set; }

        public EventLevel Level { get; set; }

        public string Message { get; set; }

        public NodeRef[] Nodes { get; set; }

        public DateTimeOffset StartTime { get; set; }
    }
}