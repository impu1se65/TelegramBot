using TelegramBot.Infrastucture.Interfaces;

namespace TelegramBot.Infrastucture.Services
{
    public class WeatherPhraseDecorator : IWeatherPhraseDecorator
    {
        private readonly IWeatherService _weatherService;

        private readonly IPhraseService _phraseService;

        public WeatherPhraseDecorator(IWeatherService weatherService, IPhraseService phraseService)
        {
            _weatherService = weatherService;
            _phraseService = phraseService;
        }
    }
}