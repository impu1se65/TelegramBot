using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TelegramBot.Core;
using TelegramBot.Core.Bot;
using TelegramBot.Core.Interfaces;
using TelegramBot.Infrastucture.Contracts;

namespace TelegramBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var appService = serviceProvider.GetService<ITelegramBotCore>();

            appService.Run();
            
            Console.ReadKey();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
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
        }
    }
}
