using System;

namespace Aims.Sdk
{
    /// <summary>
    ///   Provides convenient access to the environment-specific API methods of the AIMS Platform.
    /// </summary>
    public partial class EnvironmentApi
    {
        /// <summary>
        ///   Provides convenient access to the system-related API methods of the AIMS Platform.
        /// </summary>
        public class SystemsApi
        {
            private readonly EnvironmentApi _api;
            private readonly Uri _systemsUri;

            /// <summary>
            ///   Initializes a new instance of the <see cref="SystemsApi"/> class.
            /// </summary>
            /// <param name="api">The API accessor.</param>
            internal SystemsApi(EnvironmentApi api)
            {
                _api = api;
                _systemsUri = new Uri(_api.EnvironmentUri, "systems");
            }

            /// <summary>
            ///   Creates a system in the current environment.
            /// </summary>
            /// <param name="system">The system to create.</param>
            /// <returns>
            ///   The created system.
            /// </returns>
            /// <remarks>
            ///   <see cref="EnvironmentId"/> must be set before using this method.
            /// </remarks>
            public System Add(System system)
            {
                return _api.HttpHelper.Post<System>(_systemsUri, system);
            }

            /// <summary>
            ///   Gets all systems in the current environment.
            /// </summary>
            /// <returns>
            ///   All systems in the current environment.
            /// </returns>
            /// <remarks>
            ///   <see cref="EnvironmentId"/> must be set before using this method.
            /// </remarks>
            public System[] Get()
            {
                return _api.HttpHelper.Get<System[]>(_systemsUri);
            }
        }
    }
}