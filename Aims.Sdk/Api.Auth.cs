using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Aims.Sdk
{
    /// <summary>
    ///   Provides convenient access to the API methods of the AIMS Platform.
    /// </summary>
    public partial class Api
    {
        /// <summary>
        ///   Provides convenient access to the environment-related API methods of the AIMS Platform.
        /// </summary>
        public class AuthApi
        {
            private readonly Api _api;

            /// <summary>
            ///   Initializes a new instance of the <see cref="EnvironmentsApi"/> class.
            /// </summary>
            /// <param name="api">The API accessor.</param>
            internal AuthApi(Api api)
            {
                _api = api;
            }

            /// <summary>
            ///   Get an agent auth token that can be used to connect to a system.
            /// </summary>
            /// <param name="system">The system to get a token for.</param>
            /// <returns>
            ///   An agent auth token.
            /// </returns>
            public string GetAgentToken(System system)
            {
                var response = _api.HttpHelper.Post<TokenResponse>(new Uri(_api.Uri, "auth/token"), null,
                    new Dictionary<string, object>
                    {
                        { "response_type", "agent" },
                        { "agent_id", system.AgentId },
                        { "environment_id", system.EnvironmentId },
                        { "system_id", system.Id },
                    });
                return response != null ? response.AccessToken : null;
            }

            [DataContract]
            private class TokenResponse
            {
                [DataMember(Name = "access_token")]
                public string AccessToken { get; set; }
            }
        }
    }
}