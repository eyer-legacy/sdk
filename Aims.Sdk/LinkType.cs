using System.Runtime.Serialization;

namespace Aims.Sdk
{
    public enum LinkType
    {
        [EnumMember(Value = "hierarchy")]
        Hierarchy = 1,

        [EnumMember(Value = "binding")]
        Binding = 2,

        [EnumMember(Value = "reference")]
        Reference = 3,

        [EnumMember(Value = "dynamic")]
        Dynamic = 4,
    }
}