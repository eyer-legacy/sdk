using System;
using System.Collections.Generic;
using System.Linq;

namespace Aims.Sdk
{
    /// <summary>
    ///   Provides convenient access to the inbound API methods of the AIMS Platform.
    /// </summary>
    public class Api
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
        {
            Uri = uri;

            Environments = new EnvironmentsApi(this);
            Events = new EventsApi(this);
            Links = new LinksApi(this);
            Nodes = new NodesApi(this);
            StatPoints = new StatPointsApi(this);
            Systems = new SystemsApi(this);

            HttpHelper = new HttpHelper(credentials);
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
        ///   Gets or sets the identifier of the environment this API accessor connects to.
        ///   This property must be set before environment-specific API methods are used.
        /// </summary>
        /// <value>
        ///   The identifier of the environment this API accessor connects to.
        /// </value>
        public Guid? EnvironmentId { get; set; }

        /// <summary>
        ///   Gets an API accessor for environments.
        /// </summary>
        /// <value>
        ///   An API accessor for environments.
        /// </value>
        public EnvironmentsApi Environments { get; private set; }

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

        /// <summary>
        ///   Gets the URI pointing to the root of the API.
        /// </summary>
        /// <value>
        ///   The URI pointing to the root of the API.
        /// </value>
        public Uri Uri { get; private set; }

        /// <summary>
        ///   Gets the URI pointing to the environment-specific root of the API.
        /// </summary>
        /// <value>
        ///   The URI pointing to the environment-specific root of the API.
        /// </value>
        protected Uri EnvironmentUri
        {
            get
            {
                if (!EnvironmentId.HasValue)
                    throw new InvalidOperationException("EnvironmentId must be set before using this property.");
                return new Uri(Uri, String.Format("environments/{0}/", EnvironmentId));
            }
        }

        /// <summary>
        ///   Gets the HTTP helper of this instance.
        /// </summary>
        /// <value>
        ///   The HTTP helper of this instance.
        /// </value>
        protected HttpHelper HttpHelper { get; private set; }

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

        /// <summary>
        ///   Provides convenient access to the event-related API methods of the AIMS Platform.
        /// </summary>
        public class EventsApi
        {
            private readonly Api _api;

            /// <summary>
            ///   Initializes a new instance of the <see cref="EventsApi"/> class.
            /// </summary>
            /// <param name="api">The API accessor.</param>
            internal EventsApi(Api api)
            {
                _api = api;
            }

            /// <summary>
            ///   Sends events to the API.
            /// </summary>
            /// <param name="events">The events to send.</param>
            public void Send(Event[] events)
            {
                _api.HttpHelper.Post(new Uri(_api.EnvironmentUri, "events"), events);
            }
        }

        /// <summary>
        ///   Provides convenient access to the link-related API methods of the AIMS Platform.
        /// </summary>
        public class LinksApi
        {
            private readonly Api _api;

            /// <summary>
            ///   Initializes a new instance of the <see cref="LinksApi"/> class.
            /// </summary>
            /// <param name="api">The API accessor.</param>
            internal LinksApi(Api api)
            {
                _api = api;
            }

            /// <summary>
            ///   Sends links to the API.
            /// </summary>
            /// <param name="links">The links to send.</param>
            public void Send(Link[] links)
            {
                _api.HttpHelper.Post(new Uri(_api.EnvironmentUri, "links"), links);
            }
        }

        /// <summary>
        ///   Provides convenient access to the node-related API methods of the AIMS Platform.
        /// </summary>
        public class NodesApi
        {
            private readonly Api _api;

            /// <summary>
            ///   Initializes a new instance of the <see cref="NodesApi"/> class.
            /// </summary>
            /// <param name="api">The API accessor.</param>
            internal NodesApi(Api api)
            {
                _api = api;
            }

            /// <summary>
            ///   Gets the URI pointing to the node API.
            /// </summary>
            /// <value>
            ///   The URI pointing to the node API.
            /// </value>
            private Uri NodesUri
            {
                get { return new Uri(_api.EnvironmentUri, "nodes"); }
            }

            /// <summary>
            ///   Gets all nodes in the system.
            /// </summary>
            /// <returns>
            ///   All nodes in the system.
            /// </returns>
            public Node[] Get()
            {
                return _api.HttpHelper.Get<Node[]>(NodesUri,
                    new Dictionary<string, object> { { "include", "node-ref+node-props" } });
            }

            /// <summary>
            ///   Gets a node with the specified reference.
            /// </summary>
            /// <param name="nodeRef">The node reference to look for.</param>
            /// <returns>
            ///   A node with the specified reference.
            /// </returns>
            public Node Get(NodeRef nodeRef)
            {
                return _api.HttpHelper.Get<Node>(NodesUri, ConvertToQuery(nodeRef));
            }

            /// <summary>
            ///   Removes a node with the specified reference.
            /// </summary>
            /// <param name="nodeRef">The node reference to look for.</param>
            public void Remove(NodeRef nodeRef)
            {
                _api.HttpHelper.Delete(NodesUri, ConvertToQuery(nodeRef));
            }

            /// <summary>
            ///   Sends nodes to the API.
            /// </summary>
            /// <param name="nodes">The nodes to send.</param>
            public void Send(Node[] nodes)
            {
                _api.HttpHelper.Post(NodesUri, nodes);
            }

            private static Dictionary<string, object> ConvertToQuery(NodeRef nodeRef)
            {
                Dictionary<string, object> query = nodeRef.Parts
                    .ToDictionary(kvp => kvp.Key, kvp => (object)kvp.Value);
                query["nodeType"] = nodeRef.NodeType;

                return query;
            }
        }

        /// <summary>
        ///   Provides convenient access to the statistics-related API methods of the AIMS Platform.
        /// </summary>
        public class StatPointsApi
        {
            private readonly Api _api;

            /// <summary>
            ///   Initializes a new instance of the <see cref="StatPointsApi"/> class.
            /// </summary>
            /// <param name="api">The API accessor.</param>
            internal StatPointsApi(Api api)
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

        /// <summary>
        ///   Provides convenient access to the system-related API methods of the AIMS Platform.
        /// </summary>
        public class SystemsApi
        {
            private readonly Api _api;

            /// <summary>
            ///   Initializes a new instance of the <see cref="SystemsApi"/> class.
            /// </summary>
            /// <param name="api">The API accessor.</param>
            internal SystemsApi(Api api)
            {
                _api = api;
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
                if (!_api.EnvironmentId.HasValue)
                    throw new InvalidOperationException("EnvironmentId must be set before using this method.");
                return Get(_api.EnvironmentId.Value);
            }

            /// <summary>
            ///   Gets all systems in an environment.
            /// </summary>
            /// <returns>
            ///   All systems in an environment.
            /// </returns>
            public System[] Get(Guid environmentId)
            {
                return _api.HttpHelper.Get<System[]>(new Uri(_api.Uri, "environments/" + environmentId + "/systems"));
            }
        }
    }
}