using TelegramBot.Infrastucture.Contracts;

namespace TelegramBot.Infrastucture.Services
{
    public class PhraseService
    {
        private readonly Settings _settings;

        public PhraseService(Settings settings)
        {
            _settings = settings;
        }
    }
}