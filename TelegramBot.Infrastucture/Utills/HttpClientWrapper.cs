using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TelegramBot.Infrastucture.Interfaces;

namespace TelegramBot.Infrastucture.Utills
{
    public class HttpClientWrapper : IHttpClient
    {
        private Dictionary<string, string> _headers;

        public HttpClientWrapper()
        {
            _headers = new Dictionary<string, string>();
        }

        public void AddHeaders(string headerName, string headerValue)
        {
            _headers[headerName] = headerValue;
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
        {
            using (var client = new HttpClient())
            {
                if (_headers != null)
                {
                    foreach (var keyValuePair in _headers)
                    {
                        client.DefaultRequestHeaders.Add(keyValuePair.Key, keyValuePair.Value);
                    }
                }
                return await client.GetAsync(uri);
            }
        }
    }
}
