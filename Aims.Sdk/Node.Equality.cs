using System;

namespace Aims.Sdk
{
    public partial class Node : IEquatable<Node>
    {
        private static readonly DictionaryEqualityComparer<string, string> DictionaryEqualityComparer
            = new DictionaryEqualityComparer<string, string>();

        public bool Equals(Node other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(NodeRef, other.NodeRef)
                && IsDeleted == other.IsDeleted
                && CreationTime.Equals(other.CreationTime)
                && ModificationTime.Equals(other.ModificationTime)
                && DictionaryEqualityComparer.Equals(Properties, other.Properties);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Node)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (NodeRef != null ? NodeRef.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IsDeleted.GetHashCode();
                hashCode = (hashCode * 397) ^ CreationTime.GetHashCode();
                hashCode = (hashCode * 397) ^ ModificationTime.GetHashCode();
                hashCode = (hashCode * 397) ^ (Properties != null ? Properties.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}