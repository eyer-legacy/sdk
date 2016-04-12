using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace Aims.Sdk
{
    public class HttpHelper
    {
        private readonly string _token;
        private readonly long _systemId;

        public HttpHelper(string token, long systemId)
        {
            _token = token;
            _systemId = systemId;
        }

        private WebClient GetWebClient()
        {
            var webClient = new WebClient();
            webClient.Headers.Add(HttpRequestHeader.Authorization, "bearer " + _token);
            webClient.Headers.Add("X-System", _systemId.ToString(CultureInfo.InvariantCulture));

            return webClient;
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

        public void Delete(Uri uri, Dictionary<string, object> query)
        {
            using (WebClient client = GetWebClient())
            {
                client.UploadData(BuildUri(uri, query), "DELETE", new byte[0]);
            }
        }

        public void Post(Uri uri, object body, Dictionary<string, object> query = null)
        {
            try
            {
                using (WebClient client = GetWebClient())
                {
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");

                    var bodySerialized = body != null ? JsonConvert.SerializeObject(body) : String.Empty;
                    client.UploadString(BuildUri(uri, query), bodySerialized);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
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
    }
}