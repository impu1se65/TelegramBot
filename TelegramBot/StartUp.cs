﻿using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TelegramBot.Core.Bot;
using TelegramBot.Core.Interfaces;
using TelegramBot.Infrastucture.Contracts;
using TelegramBot.Infrastucture.Interfaces;
using TelegramBot.Infrastucture.Services;

namespace TelegramBot
{
    public class StartUp
    {
        public void Configure(IApplicationBuilder builder)
        {
            builder.Run(appContext =>
            {
                return appContext.Response.WriteAsync("App run");
            });
        }

        public static void ConfigureServices(IServiceCollection serviceCollection)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appSettings.json", optional: false)
                .Build();

            serviceCollection.AddScoped<IConfiguration>(cfg => configuration);
            serviceCollection.AddOptions();
            serviceCollection.AddScoped<IConfiguration>(cfg => configuration);
            serviceCollection.Configure<Settings>(configuration.GetSection(nameof(Settings)));
            serviceCollection.AddScoped(cfg => cfg.GetService<IOptionsSnapshot<Settings>>().Value);
            serviceCollection.AddScoped<ITelegramBotCore, TelegramBotCore>();
            serviceCollection.AddScoped<IWeatherService, WeatherService>();
            serviceCollection.AddScoped<IPhraseService, PhraseService>();
            serviceCollection.AddScoped<IWeatherPhraseFacade, WeatherPhraseFacade>();
            serviceCollection.AddScoped<ITelegramBotClientFactory, TelegramBotClientFactory>();
        }
    }
}
