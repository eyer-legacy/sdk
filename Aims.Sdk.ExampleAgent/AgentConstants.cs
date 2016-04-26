namespace Aims.Sdk.ExampleAgent
{
    public static class AgentConstants
    {
        public static class NodeType
        {
            public const string Server = "acme.int-sys.server";
        }

        public static class PropertyType
        {
            public const string ServerOs = "acme.int-sys.server-os";
        }

        public static class Service
        {
            public const string ApplicationName = "AIMS Example Agent";
            public const string EventSource = "AIMS Agent";
            public const string Log = "Application";
            public const string ServiceName = "aims-example-agent";
        }

        public static class StatType
        {
            public const string ServerCpu = "acme.int-sys.server-cpu";
        }

        public static class Status
        {
            public const string Started = "aims.core.started";
            public const string Stopped = "aims.core.stopped";
            public const string Unavailable = "acme.int-sys.unavailable";
        }
    }
}