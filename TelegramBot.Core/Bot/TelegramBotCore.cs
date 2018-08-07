using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;

        public TelegramBotCore(IWeatherPhraseFacade weatherPhraseFacade, Settings settings, ITelegramBotClientFactory factory, ILogger<TelegramBotCore> logger)
        {
            _weatherPhraseFacade = weatherPhraseFacade;
            _settings = settings;
            _logger = logger;
            //_client = factory.GeTelegramBotClient(settings.TelegramBotToken);
        }

        public void Run()
        {
            _logger.LogInformation("App start");
        }
    }
}
