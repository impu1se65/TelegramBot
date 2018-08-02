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
        private readonly IWeatherPhraseDecorator _weatherPhraseDecorator;
        private readonly Settings _settings;
        private readonly ITelegramBotClient _client;

        public TelegramBotCore(IWeatherPhraseDecorator weatherPhraseDecorator, Settings settings, ITelegramBotClient client)
        {
            _weatherPhraseDecorator = weatherPhraseDecorator;
            _settings = settings;
            _client = client;
        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}
