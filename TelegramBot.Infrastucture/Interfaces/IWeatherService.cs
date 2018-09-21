using System;
using System.Threading.Tasks;
using TelegramBot.Infrastucture.Contracts;

namespace TelegramBot.Infrastucture.Interfaces
{
    public interface IWeatherService
    {
        Task<ForecastModel> GetWeather(string city, DateTime? date);
    }
}
