using System;
using Microsoft.Extensions.DependencyInjection;
using TelegramBot.Core.Interfaces;

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

            appService.StartReceiving();

            Console.ReadKey();

            appService.StopReceiving();
        }
    }
}
