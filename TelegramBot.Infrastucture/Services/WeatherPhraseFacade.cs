using TelegramBot.Infrastucture.Interfaces;

namespace TelegramBot.Infrastucture.Services
{
    public class WeatherPhraseFacade : IWeatherPhraseFacade
    {
        private readonly IWeatherService _weatherService;

        private readonly IPhraseService _phraseService;

        public WeatherPhraseFacade(IWeatherService weatherService, IPhraseService phraseService)
        {
            _weatherService = weatherService;
            _phraseService = phraseService;
        }
    }
}