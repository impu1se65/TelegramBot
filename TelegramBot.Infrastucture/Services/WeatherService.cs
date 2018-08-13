using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using DarkSky.Models;
using DarkSky.Services;
using Geocoding;
using Geocoding.Google;
using NodaTime.TimeZones;
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

        public async Task<ForecastModel> GetWeatherNow(string city, DateTime? date)
        {
            if (string.IsNullOrEmpty(city)) // todo: think about it
            {
                city = DefaultCity;
            }

            if (date.Equals(null)) // todo: think about it
            {
                date = DateTime.UtcNow;
            }

            var addresses = await _geocoder.GeocodeAsync(city);
            var address = addresses.First();
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
                //Temperature = (5 / 9 * (forecastResult.Currently.Temperature - 32)).ToString(),
                Temperature = forecastResult.Currently.Temperature.ToString(),
                WindSpeed = forecastResult.Currently.WindSpeed.ToString(),
                Summary = forecastResult.Currently.Summary,
                Location = address.ToString(),
            };

            return await Task.FromResult(forecast);
        }
    }
}

