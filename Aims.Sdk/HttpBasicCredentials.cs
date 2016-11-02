using System;
using System.Text;

namespace Aims.Sdk
{
    public class HttpBasicCredentials : HttpCredentials
    {
        public HttpBasicCredentials(string username, string password)
            : base("Basic", GetParameter(username, password))
        {
        }

        private static string GetParameter(string username, string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(String.Join(":", new[] { username, password })));
        }
    }
}