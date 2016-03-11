using System;

namespace Aims.Sdk
{
    public partial class NodeRef : IEquatable<NodeRef>
    {
        private static readonly DictionaryEqualityComparer<string, string> DictionaryEqualityComparer
            = new DictionaryEqualityComparer<string, string>();

        public bool Equals(NodeRef other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return String.Equals(NodeType, other.NodeType)
                && DictionaryEqualityComparer.Equals(Parts, other.Parts);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((NodeRef)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((NodeType != null ? NodeType.GetHashCode() : 0) * 397)
                    ^ (Parts != null ? DictionaryEqualityComparer.GetHashCode(Parts) : 0);
            }
        }
    }
}