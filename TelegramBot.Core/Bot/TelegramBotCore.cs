using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using TelegramBot.Core.Interfaces;
using TelegramBot.Infrastucture.Contracts;
using TelegramBot.Infrastucture.Interfaces;

namespace TelegramBot.Core.Bot
{
    public class TelegramBotCore : ITelegramBotCore
    {
        private readonly IWeatherPhraseFacade _weatherPhraseFacade;
        private readonly Settings _settings;
        private readonly ITelegramBotClient _client;

        public TelegramBotCore(IWeatherPhraseFacade weatherPhraseFacade, Settings settings, ITelegramBotClientFactory factory)
        {
            _weatherPhraseFacade = weatherPhraseFacade;
            _settings = settings;
            _client = factory.GeTelegramBotClient(settings.TelegramBotToken);
        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}
