using System;

namespace Aims.Sdk
{
    /// <summary>
    ///   Provides convenient access to the API methods of the AIMS Platform.
    /// </summary>
    public abstract class ApiBase
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="Api"/> class.
        /// </summary>
        /// <param name="uri">The URI pointing to the root of the API.</param>
        /// <param name="credentials">The authentication credentials.</param>
        protected ApiBase(Uri uri, HttpCredentials credentials)
        {
            Uri = uri;
            HttpHelper = new HttpHelper(credentials);
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="ApiBase"/> class
        ///   configured the same way as <see cref="api"/>.
        /// </summary>
        /// <param name="api">The API wrapper.</param>
        protected ApiBase(ApiBase api)
        {
            Uri = api.Uri;
            HttpHelper = api.HttpHelper;
        }

        /// <summary>
        ///   Gets the URI pointing to the root of the API.
        /// </summary>
        /// <value>
        ///   The URI pointing to the root of the API.
        /// </value>
        public Uri Uri { get; protected set; }

        /// <summary>
        ///   Gets the HTTP helper of this instance.
        /// </summary>
        /// <value>
        ///   The HTTP helper of this instance.
        /// </value>
        protected HttpHelper HttpHelper { get; private set; }
    }
}