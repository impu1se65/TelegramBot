namespace TelegramBot.Infrastucture.Contracts
{
    public class Entity
    {
        public string EntityType { get; set; }

        public string Type { get; set; }

        public int StartIndex { get; set; }

        public int EndIndex { get; set; }

        public double Score { get; set; }

        public Resolution Resolution { get; set; }
    }
}