using System;

namespace Aims.Sdk
{
    public class System
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string AgentId { get; set; }

        public string Version { get; set; }

        public override string ToString()
        {
            return String.Format(@"#{0}, {1} {2} ""{3}""", Id, AgentId, Version, Name);
        }
    }
}