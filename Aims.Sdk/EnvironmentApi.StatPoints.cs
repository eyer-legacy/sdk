using System;

namespace Aims.Sdk
{
    /// <summary>
    ///   Provides convenient access to the environment-specific API methods of the AIMS Platform.
    /// </summary>
    public partial class EnvironmentApi
    {
        /// <summary>
        ///   Provides convenient access to the statistics-related API methods of the AIMS Platform.
        /// </summary>
        public class StatPointsApi
        {
            private readonly EnvironmentApi _api;

            /// <summary>
            ///   Initializes a new instance of the <see cref="StatPointsApi"/> class.
            /// </summary>
            /// <param name="api">The API accessor.</param>
            internal StatPointsApi(EnvironmentApi api)
            {
                _api = api;
            }

            /// <summary>
            ///   Sends stat points to the API.
            /// </summary>
            /// <param name="points">The stat points to send.</param>
            public void Send(StatPoint[] points)
            {
                _api.HttpHelper.Post(new Uri(_api.EnvironmentUri, "statPoints"), points);
            }
        }
    }
}