using System.Collections.Generic;

namespace TelegramBot.Infrastucture.Contracts
{
    public class RootObject
    {
        public string Query { get; set; }

        public TopScoringIntent TopScoringIntent { get; set; }

        public List<Intent> Intents { get; set; }

        public List<Entity> Entities { get; set; }
    }
}