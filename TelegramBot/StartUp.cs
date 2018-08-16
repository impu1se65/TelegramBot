using System;
using System.IO;
using DarkSky.Services;
using Geocoding;
using Geocoding.Google;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TelegramBot.Core.Bot;
using TelegramBot.Core.Interfaces;
using TelegramBot.Infrastucture.Contracts;
using TelegramBot.Infrastucture.Interfaces;
using TelegramBot.Infrastucture.Services;
using TelegramBot.Infrastucture.Utills;

namespace TelegramBot
{
    public class StartUp
    {
        public static void ConfigureServices(IServiceCollection serviceCollection)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appSettings.json", optional: false)
                .Build();

            serviceCollection.AddLogging(config =>
            {
                config.AddConsole().SetMinimumLevel(LogLevel.Information);
                config.AddFilter("Microsoft", LogLevel.Error);
            });
            serviceCollection.AddOptions();
            serviceCollection.AddScoped<IConfiguration>(cfg => configuration);
            
            var telegramBotToken = configuration.GetSection(nameof(TelegramBotToken)).Get<TelegramBotToken>();
            serviceCollection.AddSingleton(telegramBotToken);
            var forecastSettings = configuration.GetSection(nameof(ForecastSettings)).Get<ForecastSettings>();
            serviceCollection.AddSingleton(forecastSettings);
            var luisSettings = configuration.GetSection(nameof(LuisSettings)).Get<LuisSettings>();
            serviceCollection.AddSingleton(luisSettings);

            serviceCollection.AddScoped<IJsonConvertWrapper, JsonConvertWrapper>();
            serviceCollection.AddScoped<Infrastucture.Interfaces.IHttpClient, HttpClientWrapper>();
            serviceCollection.AddScoped<ITelegramBotCore, TelegramBotCore>();

            serviceCollection.AddScoped<IDarkSkyService>(
                darkSky => new DarkSkyServiceWrapper(new DarkSkyService(forecastSettings.DarkSkyApiToken)));
            serviceCollection.AddScoped<IGeocoder>(geocoder => new GoogleGeocoder(forecastSettings.GoogleApiToken));

            serviceCollection.AddScoped<IWeatherService, WeatherService>();
            serviceCollection.AddScoped<IPhraseService, PhraseService>();
            serviceCollection.AddScoped<IWeatherPhraseFacade, WeatherPhraseFacade>();
            serviceCollection.AddScoped<ITelegramBotClientFactory, TelegramBotClientFactory>();
        }
    }
}
