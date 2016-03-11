using System.Collections.Generic;

namespace Aims.Sdk
{
    public partial class NodeRef
    {
        public string NodeType { get; set; }

        public Dictionary<string, string> Parts { get; set; }
    }
}