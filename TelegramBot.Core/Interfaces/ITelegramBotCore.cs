namespace TelegramBot.Core.Interfaces
{
    public interface ITelegramBotCore
    {
        void StartReceiving();

        void StopReceiving();
    }
}