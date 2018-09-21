using Telegram.Bot;

namespace TelegramBot.Infrastucture.Interfaces
{
    public interface ITelegramBotClientFactory
    {
        TelegramBotClient GeTelegramBotClient(string token);
    }
}