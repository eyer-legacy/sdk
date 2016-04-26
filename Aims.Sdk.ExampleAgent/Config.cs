using System;
using System.Configuration;

namespace Aims.Sdk.ExampleAgent
{
    public static class Config
    {
        public static string ApiEndPoint
        {
            get { return ConfigurationManager.AppSettings["api-endpoint"]; }
        }

        public static Guid EnvironmentId
        {
            get
            {
                Guid value;
                if (!Guid.TryParse(ConfigurationManager.AppSettings["environment-id"], out value))
                    throw new FormatException("'environment-id' setting has invalid format.");
                return value;
            }
        }

        public static long SystemId
        {
            get
            {
                long value;
                if (!Int64.TryParse(ConfigurationManager.AppSettings["system-id"], out value))
                    throw new FormatException("'system-id' setting has invalid format.");
                return value;
            }
        }

        public static string Token
        {
            get { return ConfigurationManager.AppSettings["token"]; }
        }
    }
}