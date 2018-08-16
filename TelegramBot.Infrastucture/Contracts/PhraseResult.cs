using System;

namespace TelegramBot.Infrastucture.Contracts
{
    public class PhraseResult
    {
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string TopIntent { get; set; }
        public double Score { get; set; }
    }
}