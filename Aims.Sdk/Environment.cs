using System;

namespace Aims.Sdk
{
    public class Environment
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public long CompanyId { get; set; }

        public override string ToString()
        {
            return String.Format(@"{0} ""{1}"" {{{2}}}", Name, DisplayName, Id);
        }
    }
}