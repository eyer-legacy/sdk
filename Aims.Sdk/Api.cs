using System;
using System.Collections.Generic;
using System.Linq;

namespace Aims.Sdk
{
    public class Api
    {
        public Api(Uri uri, string token, long systemId)
        {
            Uri = uri;

            Events = new EventsApi(this);
            Links = new LinksApi(this);
            Nodes = new NodesApi(this);
            StatPoints = new StatPointsApi(this);

            HttpHelper = new HttpHelper(token, systemId);
        }

        public EventsApi Events { get; private set; }

        public LinksApi Links { get; private set; }

        public NodesApi Nodes { get; private set; }

        public StatPointsApi StatPoints { get; private set; }

        public Uri Uri { get; private set; }

        protected HttpHelper HttpHelper { get; private set; }

        public class EventsApi
        {
            private readonly Api _api;

            public EventsApi(Api api)
            {
                _api = api;
            }

            public void Send(Event[] events)
            {
                _api.HttpHelper.Post(new Uri(_api.Uri + "/events"), events);
            }
        }

        public class LinksApi
        {
            private readonly Api _api;

            public LinksApi(Api api)
            {
                _api = api;
            }

            public void Send(Link[] links)
            {
                _api.HttpHelper.Post(new Uri(_api.Uri + "/links"), links);
            }
        }

        public class NodesApi
        {
            private readonly Api _api;
            private readonly Uri _nodesUri;

            public NodesApi(Api api)
            {
                _api = api;
                _nodesUri = new Uri(_api.Uri + "/nodes");
            }

            public Node Get(NodeRef nodeRef)
            {
                return _api.HttpHelper.Get<Node>(_nodesUri, ConvertToQuery(nodeRef));
            }

            public Node[] Get()
            {
                return _api.HttpHelper.Get<Node[]>(_nodesUri,
                    new Dictionary<string, object> { { "include", "node-ref+node-props" } });
            }

            public void Remove(NodeRef nodeRef)
            {
                _api.HttpHelper.Delete(_nodesUri, ConvertToQuery(nodeRef));
            }

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

        public class StatPointsApi
        {
            private readonly Api _api;

            public StatPointsApi(Api api)
            {
                _api = api;
            }

            public void Send(StatPoint[] points)
            {
                _api.HttpHelper.Post(new Uri(_api.Uri + "/statPoints"), points);
            }
        }
    }
}