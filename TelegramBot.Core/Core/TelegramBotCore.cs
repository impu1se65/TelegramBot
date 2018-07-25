using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using TelegramBot.Core.Interfaces;

namespace TelegramBot.Core.Core
{
    public class TelegramBotCore : ITelegramBotCore
    {
        public TelegramBotCore(Settings settings)
        {
            var s = settings;
        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}
