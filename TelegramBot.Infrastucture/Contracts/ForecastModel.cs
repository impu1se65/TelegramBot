namespace TelegramBot.Infrastucture.Contracts
{
    public class ForecastModel
    {
        public string Temperature { get; set; }
        public string WindSpeed { get; set; }
        public string Summary { get; set; }
        public string Location { get; set; }
    }
}
