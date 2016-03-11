using System.Runtime.Serialization;

namespace Aims.Sdk
{
    public enum EventLevel
    {
        [EnumMember(Value = "unspecified")]
        Unspecified = 0,

        [EnumMember(Value = "verbose")]
        Verbose,

        [EnumMember(Value = "info")]
        Informational,

        [EnumMember(Value = "warning")]
        Warning,

        [EnumMember(Value = "error")]
        Error,

        [EnumMember(Value = "critical")]
        Critical,
    }
}