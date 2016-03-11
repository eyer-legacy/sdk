using System;

namespace Aims.Sdk
{
    public class Api
    {
        private readonly Uri _uri;

        public Api(Uri uri)
        {
            _uri = uri;

            Events = new EventsApi(this);
            Nodes = new NodesApi(this);
            StatPoints = new StatPointsApi(this);
        }

        public EventsApi Events { get; private set; }

        public NodesApi Nodes { get; private set; }

        public StatPointsApi StatPoints { get; private set; }

        public class EventsApi
        {
            private readonly Api _api;

            public EventsApi(Api api)
            {
                _api = api;
            }

            public void Send(Event[] points)
            {
                throw new NotImplementedException();
            }
        }

        public class NodesApi
        {
            private readonly Api _api;

            public NodesApi(Api api)
            {
                _api = api;
            }

            public Node Get(NodeRef nodeRef)
            {
                throw new NotImplementedException();
            }

            public Node[] Get()
            {
                throw new NotImplementedException();
            }

            public void Send(Node[] nodes)
            {
                throw new NotImplementedException();
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
                throw new NotImplementedException();
            }
        }
    }
}