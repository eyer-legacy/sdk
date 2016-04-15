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
        public static readonly Uri DefaultAddress = new Uri("https://beta-api.aimsinnovation.com/");

        /// <summary>
        ///   Initializes a new instance of the <see cref="Api"/> class, pointed to the default API address.
        /// </summary>
        /// <param name="token">The authentication token.</param>
        /// <param name="systemId">The identifier of the system that is to be accessed.</param>
        public Api(string token, long systemId)
            : this(DefaultAddress, token, systemId)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="Api"/> class.
        /// </summary>
        /// <param name="uri">The URI poiting to the root of the API.</param>
        /// <param name="token">The authentication token.</param>
        /// <param name="systemId">The identifier of the system that is to be accessed.</param>
        public Api(Uri uri, string token, long systemId)
        {
            Uri = uri;

            Events = new EventsApi(this);
            Links = new LinksApi(this);
            Nodes = new NodesApi(this);
            StatPoints = new StatPointsApi(this);

            HttpHelper = new HttpHelper(token, systemId);
        }

        /// <summary>
        ///   Gets an API accessor for events.
        /// </summary>
        /// <value>
        ///   An API accessor for events.
        /// </value>
        public EventsApi Events { get; private set; }

        /// <summary>
        ///   Gets an API accessor for links between nodes.
        /// </summary>
        /// <value>
        ///   An API accessor for links between nodes.
        /// </value>
        public LinksApi Links { get; private set; }

        /// <summary>
        ///   Gets an API accessor for nodes.
        /// </summary>
        /// <value>
        ///   An API accessor for nodes.
        /// </value>
        public NodesApi Nodes { get; private set; }

        /// <summary>
        ///   Gets an API accessor for statistics.
        /// </summary>
        /// <value>
        ///   An API accessor for statistics.
        /// </value>
        public StatPointsApi StatPoints { get; private set; }

        /// <summary>
        ///   Gets the URI poiting to the root of the API.
        /// </summary>
        /// <value>
        ///   The URI poiting to the root of the API.
        /// </value>
        public Uri Uri { get; private set; }

        /// <summary>
        ///   Gets the HTTP helper of this instance.
        /// </summary>
        /// <value>
        ///   The HTTP helper of this instance.
        /// </value>
        protected HttpHelper HttpHelper { get; private set; }

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
                _api.HttpHelper.Post(new Uri(_api.Uri + "/events"), events);
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
                _api.HttpHelper.Post(new Uri(_api.Uri + "/links"), links);
            }
        }

        /// <summary>
        ///   Provides convenient access to the node-related API methods of the AIMS Platform.
        /// </summary>
        public class NodesApi
        {
            private readonly Api _api;
            private readonly Uri _nodesUri;

            /// <summary>
            ///   Initializes a new instance of the <see cref="NodesApi"/> class.
            /// </summary>
            /// <param name="api">The API accessor.</param>
            internal NodesApi(Api api)
            {
                _api = api;
                _nodesUri = new Uri(_api.Uri + "/nodes");
            }

            /// <summary>
            ///   Gets all nodes in the system.
            /// </summary>
            /// <returns>
            ///   All nodes in the system.
            /// </returns>
            public Node[] Get()
            {
                return _api.HttpHelper.Get<Node[]>(_nodesUri,
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
                return _api.HttpHelper.Get<Node>(_nodesUri, ConvertToQuery(nodeRef));
            }

            /// <summary>
            ///   Removes a node with the specified reference.
            /// </summary>
            /// <param name="nodeRef">The node reference to look for.</param>
            public void Remove(NodeRef nodeRef)
            {
                _api.HttpHelper.Delete(_nodesUri, ConvertToQuery(nodeRef));
            }

            /// <summary>
            ///   Sends nodes to the API.
            /// </summary>
            /// <param name="nodes">The nodes to send.</param>
            public void Send(Node[] nodes)
            {
                _api.HttpHelper.Post(_nodesUri, nodes);
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
                _api.HttpHelper.Post(new Uri(_api.Uri + "/statPoints"), points);
            }
        }
    }
}