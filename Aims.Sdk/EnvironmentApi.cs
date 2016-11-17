using System;

namespace Aims.Sdk
{
    /// <summary>
    ///   Provides convenient access to the environment-specific API methods of the AIMS Platform.
    /// </summary>
    public partial class EnvironmentApi : ApiBase
    {
        private readonly Guid _environmentId;
        private readonly Uri _environmentUri;

        /// <summary>
        ///   Initializes a new instance of the <see cref="EnvironmentApi"/> class
        ///   configured the same way as <see cref="api"/>.
        /// </summary>
        /// <param name="api">The API wrapper.</param>
        /// <param name="environmentId">The identifier of an environment to point to.</param>
        internal EnvironmentApi(ApiBase api, Guid environmentId)
            : base(api)
        {
            _environmentId = environmentId;
            _environmentUri = new Uri(Uri, String.Format("environments/{0}/", EnvironmentId));

            Events = new EventsApi(this);
            Links = new LinksApi(this);
            Nodes = new NodesApi(this);
            StatPoints = new StatPointsApi(this);
            Systems = new SystemsApi(this);
        }

        /// <summary>
        ///   Gets the identifier of the environment this API accessor connects to.
        ///   This property must be set before environment-specific API methods are used.
        /// </summary>
        /// <value>
        ///   The identifier of the environment this API accessor connects to.
        /// </value>
        public Guid EnvironmentId
        {
            get { return _environmentId; }
        }

        /// <summary>
        ///   Gets the URI pointing to the environment-specific root of the API.
        /// </summary>
        /// <value>
        ///   The URI pointing to the environment-specific root of the API.
        /// </value>
        public Uri EnvironmentUri
        {
            get { return _environmentUri; }
        }

        /// <summary>
        ///   Gets an API accessor for events.
        /// </summary>
        /// <value>
        ///   An API accessor for events.
        /// </value>
        /// <remarks>
        ///   <see cref="EnvironmentId"/> must be set before using this property.
        /// </remarks>
        public EventsApi Events { get; private set; }

        /// <summary>
        ///   Gets an API accessor for links between nodes.
        /// </summary>
        /// <value>
        ///   An API accessor for links between nodes.
        /// </value>
        /// <remarks>
        ///   <see cref="EnvironmentId"/> must be set before using this property.
        /// </remarks>
        public LinksApi Links { get; private set; }

        /// <summary>
        ///   Gets an API accessor for nodes.
        /// </summary>
        /// <value>
        ///   An API accessor for nodes.
        /// </value>
        /// <remarks>
        ///   <see cref="EnvironmentId"/> must be set before using this property.
        /// </remarks>
        public NodesApi Nodes { get; private set; }

        /// <summary>
        ///   Gets an API accessor for statistics.
        /// </summary>
        /// <value>
        ///   An API accessor for statistics.
        /// </value>
        /// <remarks>
        ///   <see cref="EnvironmentId"/> must be set before using this property.
        /// </remarks>
        public StatPointsApi StatPoints { get; private set; }

        /// <summary>
        ///   Gets an API accessor for systems.
        /// </summary>
        /// <value>
        ///   An API accessor for systems.
        /// </value>
        public SystemsApi Systems { get; private set; }
    }
}