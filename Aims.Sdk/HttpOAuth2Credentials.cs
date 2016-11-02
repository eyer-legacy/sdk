namespace Aims.Sdk
{
    public class HttpOAuth2Credentials : HttpCredentials
    {
        public HttpOAuth2Credentials(string token)
            : base("Bearer", token)
        {
        }
    }
}