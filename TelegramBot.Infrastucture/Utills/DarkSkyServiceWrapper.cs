using System.Threading.Tasks;
using DarkSky.Models;
using DarkSky.Services;
using TelegramBot.Infrastucture.Interfaces;

namespace TelegramBot.Infrastucture.Utills
{
    public class DarkSkyServiceWrapper: IDarkSkyService
    {
        private readonly DarkSkyService _service;

        public DarkSkyServiceWrapper(DarkSkyService service)
        {
            _service = service;
        }

        public async Task<DarkSkyResponse> GetForecast(double latitude, double longitude, DarkSkyService.OptionalParameters options)
        {
            return options != null
                ? await _service.GetForecast(latitude, longitude, options)
                : await _service.GetForecast(latitude, longitude);
        }
    }
}
