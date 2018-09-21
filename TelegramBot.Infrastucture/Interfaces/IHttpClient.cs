using System.Net.Http;
using System.Threading.Tasks;

namespace TelegramBot.Infrastucture.Interfaces
{
    public interface IHttpClient
    {
        void AddHeaders(string headerName, string headerValue);
        Task<HttpResponseMessage> GetAsync(string uri);
    }
}