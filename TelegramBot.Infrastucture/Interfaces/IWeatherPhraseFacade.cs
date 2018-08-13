using System.Threading.Tasks;

namespace TelegramBot.Infrastucture.Interfaces
{
    public interface IWeatherPhraseFacade
    {
        Task<string> GetForecast(string query, string userName);
    }
}