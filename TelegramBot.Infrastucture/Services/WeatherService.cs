using TelegramBot.Infrastucture.Contracts;
using TelegramBot.Infrastucture.Interfaces;

namespace TelegramBot.Infrastucture.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly Settings _settings;

        public WeatherService(Settings settings)
        {
            _settings = settings;
        }
    }
}
