using System;

namespace Aims.Sdk
{
    /// <summary>
    ///   Provides convenient access to the API methods of the AIMS Platform.
    /// </summary>
    public partial class Api : ApiBase
    {
        /// <summary>
        ///   The default API address of the AIMS Platform.
        /// </summary>
        public static readonly Uri DefaultAddress = new Uri("https://api.aimsinnovation.com/api/");

        /// <summary>
        ///   Initializes a new instance of the <see cref="Api"/> class,
        ///   pointed to the default API address.
        /// </summary>
        /// <param name="credentials">The authentication credentials.</param>
        public Api(HttpCredentials credentials)
            : this(DefaultAddress, credentials)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="Api"/> class.
        /// </summary>
        /// <param name="uri">The URI pointing to the root of the API.</param>
        /// <param name="credentials">The authentication credentials.</param>
        public Api(Uri uri, HttpCredentials credentials)
            : base(uri, credentials)
        {
            Auth = new AuthApi(this);
            Environments = new EnvironmentsApi(this);
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="Api"/> class using an OAuth2 token,
        ///   pointed to the default API address.
        /// </summary>
        /// <param name="token">The OAuth2 token.</param>
        public Api(string token)
            : this(DefaultAddress, new HttpOAuth2Credentials(token))
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="Api"/> class using an OAuth2 token.
        /// </summary>
        /// <param name="uri">The URI pointing to the root of the API.</param>
        /// <param name="token">The OAuth2 token.</param>
        public Api(Uri uri, string token)
            : this(uri, new HttpOAuth2Credentials(token))
        {
        }

        /// <summary>
        ///   Gets an API accessor for authentication.
        /// </summary>
        /// <value>
        ///   An API accessor for authentication.
        /// </value>
        public AuthApi Auth { get; private set; }

        /// <summary>
        ///   Gets an API accessor for environments.
        /// </summary>
        /// <value>
        ///   An API accessor for environments.
        /// </value>
        public EnvironmentsApi Environments { get; private set; }

        /// <summary>
        ///   Gets an API accessor to an environment.
        /// </summary>
        /// <param name="environmentId">The environment identifier.</param>
        /// <returns>
        ///   An API accessor to the environment.
        /// </returns>
        public EnvironmentApi ForEnvironment(Guid environmentId)
        {
            return new EnvironmentApi(this, environmentId);
        }
    }
}