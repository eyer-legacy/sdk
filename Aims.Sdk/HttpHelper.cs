using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace Aims.Sdk
{
    public class HttpHelper
    {
        private readonly string _token;

        public HttpHelper(string token)
        {
            _token = token;
        }

        public void Delete(Uri uri, Dictionary<string, object> query)
        {
            using (WebClient client = GetWebClient())
            {
                client.UploadData(BuildUri(uri, query), "DELETE", new byte[0]);
            }
        }

        public T Get<T>(Uri uri, Dictionary<string, object> query = null)
        {
            try
            {
                using (WebClient client = GetWebClient())
                {
                    var response = client.DownloadString(BuildUri(uri, query));
                    return JsonConvert.DeserializeObject<T>(response);
                }
            }
            catch (WebException ex)
            {
                var response = ex.Response as HttpWebResponse;
                if (response != null && response.StatusCode == HttpStatusCode.NotFound)
                    return default(T);

                throw;
            }
        }

        public void Post(Uri uri, object body, Dictionary<string, object> query = null)
        {
            PostInternal(uri, body, query);
        }

        public T Post<T>(Uri uri, object body, Dictionary<string, object> query = null)
        {
            string response = PostInternal(uri, body, query);
            return JsonConvert.DeserializeObject<T>(response);
        }

        private static string BuildUri(Uri uri, Dictionary<string, object> query = null)
        {
            if (query != null)
            {
                return uri + "?" + String.Join("&", query
                    .Select(kvp => kvp.Key + "=" + Uri.EscapeUriString(kvp.Value.ToString()))
                    .ToArray());
            }

            return uri.ToString();
        }

        private WebClient GetWebClient()
        {
            var webClient = new WebClient();
            webClient.Headers.Add(HttpRequestHeader.Authorization, "bearer " + _token);

            return webClient;
        }

        private string PostInternal(Uri uri, object body, Dictionary<string, object> query)
        {
            using (WebClient client = GetWebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");

                string bodySerialized = body != null ? JsonConvert.SerializeObject(body) : String.Empty;
                return client.UploadString(BuildUri(uri, query), bodySerialized);
            }
        }
    }
}