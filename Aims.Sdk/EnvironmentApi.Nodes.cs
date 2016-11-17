using System;
using System.Collections.Generic;
using System.Linq;

namespace Aims.Sdk
{
    /// <summary>
    ///   Provides convenient access to the environment-specific API methods of the AIMS Platform.
    /// </summary>
    public partial class EnvironmentApi
    {
        /// <summary>
        ///   Provides convenient access to the node-related API methods of the AIMS Platform.
        /// </summary>
        public class NodesApi
        {
            private readonly EnvironmentApi _api;
            private readonly Uri _nodesUri;

            /// <summary>
            ///   Initializes a new instance of the <see cref="NodesApi"/> class.
            /// </summary>
            /// <param name="api">The API accessor.</param>
            internal NodesApi(EnvironmentApi api)
            {
                _api = api;
                _nodesUri = new Uri(_api.EnvironmentUri, "nodes");
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
    }
}