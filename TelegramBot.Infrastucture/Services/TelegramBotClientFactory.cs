using Telegram.Bot;
using TelegramBot.Infrastucture.Interfaces;

namespace TelegramBot.Infrastucture.Services
{
    public class TelegramBotClientFactory : ITelegramBotClientFactory
    {
        public TelegramBotClient GeTelegramBotClient(string token)
        {
            return new TelegramBotClient(token);
        }
    }
}