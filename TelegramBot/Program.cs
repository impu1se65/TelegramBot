using System;
using System.IO;
using System.Reflection.Metadata;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            StartUp.ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var appService = serviceProvider.GetService<ITelegramBotCore>();
            var logger = serviceProvider.GetService<ILogger<Program>>();
            logger.LogInformation("eqweqwe");
            appService.Run();

            Console.ReadKey();
        }
    }
}
