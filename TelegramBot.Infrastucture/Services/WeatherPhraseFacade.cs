﻿using System;
using System.Threading.Tasks;
using TelegramBot.Infrastucture.Contracts;
using TelegramBot.Infrastucture.Interfaces;

namespace TelegramBot.Infrastucture.Services
{
    public class WeatherPhraseFacade : IWeatherPhraseFacade
    {
        private readonly IWeatherService _weatherService;
        private readonly IPhraseService _phraseService;
        private const double MinPhraseScore = 0.64;
        private const string WeatherEntityName = "Weather.GetForecast";
        private const string DontUnderstandError = "I`m don`t understand you, please provide valid weather request";

        public WeatherPhraseFacade(IWeatherService weatherService, IPhraseService phraseService)
        {
            _weatherService = weatherService;
            _phraseService = phraseService;
        }

        public async Task<string> GetForecast(string query, string userName)
        {
            var phraseServiceResult = await _phraseService.MakeRequest(query);
            if (phraseServiceResult.TopIntent == WeatherEntityName && phraseServiceResult.Score > MinPhraseScore)
            {
                var weatherServiceResult = 
                    await _weatherService.GetWeather(phraseServiceResult.Location, phraseServiceResult.Date);

                return BuildResponse(weatherServiceResult, phraseServiceResult, userName);
            }

            return DontUnderstandError;
        }

        private string BuildResponse(ForecastModel model,PhraseResult phraseResult, string userName)
        {
            var result =    $"Hey, {userName}  {Environment.NewLine}" +
                            $"Weather in  {model.Location}  {Environment.NewLine}" +
                            $"Date: {phraseResult.Date:dd.MM.yyyy} {Environment.NewLine}" +
                            $"Temperature:  {model.Temperature} °C{Environment.NewLine}" +
                            $"Wind speed:  {model.WindSpeed} mph{Environment.NewLine} Summary: {model.Summary}";

            return result;
        }
    }
}