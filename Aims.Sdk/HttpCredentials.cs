using System;
using System.Net;

namespace Aims.Sdk
{
    public abstract class HttpCredentials
    {
        private readonly string _scheme;
        private readonly string _parameter;

        protected HttpCredentials(string scheme, string parameter)
        {
            _scheme = scheme;
            _parameter = parameter;
        }

        public virtual void SetHeaders(WebHeaderCollection headers)
        {
            headers.Add(HttpRequestHeader.Authorization, String.Join(" ", new[] { _scheme, _parameter }));
        }
    }
}