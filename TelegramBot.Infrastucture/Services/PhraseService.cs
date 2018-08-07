using TelegramBot.Infrastucture.Contracts;
using TelegramBot.Infrastucture.Interfaces;

namespace TelegramBot.Infrastucture.Services
{
    public class PhraseService : IPhraseService
    {
        private readonly Settings _settings;

        public PhraseService(Settings settings)
        {
            _settings = settings;
        }
    }
}