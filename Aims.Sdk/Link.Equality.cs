using System;

namespace Aims.Sdk
{
    public partial class Link : IEquatable<Link>
    {
        public bool Equals(Link other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(From, other.From) && Equals(To, other.To) && LinkType == other.LinkType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as Link;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = LinkType.GetHashCode();
                hashCode = (hashCode * 397) ^ (From != null ? From.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (To != null ? To.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}