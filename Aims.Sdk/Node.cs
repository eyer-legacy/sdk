using System;
using System.Collections.Generic;

namespace Aims.Sdk
{
    public partial class Node
    {
        public DateTimeOffset? CreationTime { get; set; }

        public bool IsDeleted { get; set; }

        public DateTimeOffset ModificationTime { get; set; }

        public string Name { get; set; }

        public NodeRef NodeRef { get; set; }

        public Dictionary<string, string> Properties { get; set; }

        public string Status { get; set; }
    }
}