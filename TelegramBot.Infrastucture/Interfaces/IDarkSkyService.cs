using System.Threading.Tasks;
using DarkSky.Models;
using DarkSky.Services;

namespace TelegramBot.Infrastucture.Interfaces
{
    public interface IDarkSkyService
    {
        Task<DarkSkyResponse> GetForecast(double latitude, double longitude, DarkSkyService.OptionalParameters options);
    }
}