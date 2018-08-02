using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using TelegramBot.Core.Interfaces;
using TelegramBot.Infrastucture.Contracts;
using TelegramBot.Infrastucture.Interfaces;

namespace TelegramBot.Core.Bot
{
    public class TelegramBotCore : ITelegramBotCore
    {
        private readonly IWeatherPhraseDecorator _weatherPhraseDecorator;
        private readonly Settings _settings;

        public TelegramBotCore(IWeatherPhraseDecorator weatherPhraseDecorator, Settings settings)
        {
            _weatherPhraseDecorator = weatherPhraseDecorator;
            _settings = settings;
        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}
