using System;

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
        public class EnvironmentsApi
        {
            private readonly Api _api;

            /// <summary>
            ///   Initializes a new instance of the <see cref="EnvironmentsApi"/> class.
            /// </summary>
            /// <param name="api">The API accessor.</param>
            internal EnvironmentsApi(Api api)
            {
                _api = api;
            }

            /// <summary>
            ///   Gets all available environments.
            /// </summary>
            /// <returns>
            ///   All available environments.
            /// </returns>
            public Environment[] Get()
            {
                return _api.HttpHelper.Get<Environment[]>(new Uri(_api.Uri, "environments"));
            }
        }
    }
}