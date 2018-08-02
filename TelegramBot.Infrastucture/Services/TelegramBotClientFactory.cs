using Telegram.Bot;

namespace TelegramBot.Infrastucture.Services
{
    public static class TelegramBotClientFactory
    {
        public static ITelegramBotClient GetClient(string token)
        {
           return new TelegramBotClient(token);
        }
    }
}