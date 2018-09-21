using System;
using System.Linq;
using System.Threading.Tasks;
using DarkSky.Services;
using Geocoding;
using TelegramBot.Infrastucture.Contracts;
using TelegramBot.Infrastucture.Interfaces;

namespace TelegramBot.Infrastucture.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IDarkSkyService _client;
        private readonly IGeocoder _geocoder;
        private const string DefaultCity = "Kharkiv,Ukraine";

        public WeatherService(IDarkSkyService client ,IGeocoder geocoder)
        {
            _client = client;
            _geocoder = geocoder;
        }

        public async Task<ForecastModel> GetWeather(string city, DateTime? date)
        {
            if (string.IsNullOrEmpty(city)) // todo: think about it
            {
                city = DefaultCity;
            }

            if (date.Equals(null)) // todo: think about it
            {
                date = DateTime.UtcNow;
            }


            var address = GetAddressFromString(city);
            var forecastOptions = new DarkSkyService.OptionalParameters
            {
                MeasurementUnits = "si",
                ForecastDateTime = date
            };
            var response =
                await  _client.GetForecast(address.Coordinates.Latitude, address.Coordinates.Longitude, forecastOptions);

            var forecastResult = response.Response;
            var  forecast = new ForecastModel
            {
                Temperature = forecastResult.Currently.Temperature.ToString(),
                WindSpeed = forecastResult.Currently.WindSpeed.ToString(),
                Summary = forecastResult.Currently.Summary,
                Location = address.ToString(),
            };

            return await Task.FromResult(forecast);
        }

        private Address GetAddressFromString(string city)
        {
            var addresses = _geocoder.GeocodeAsync(city).Result;
            var address = addresses.First();

            return address;
        }
    }
}

