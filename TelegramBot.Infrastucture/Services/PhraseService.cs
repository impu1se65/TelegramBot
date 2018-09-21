using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Web;
using TelegramBot.Infrastucture.Contracts;
using TelegramBot.Infrastucture.Interfaces;

namespace TelegramBot.Infrastucture.Services
{
    public class PhraseService : IPhraseService
    {
        private readonly LuisSettings _luisSettings;
        private readonly IJsonConvertWrapper _jsonConvert;
        private readonly IHttpClient _httpClient;
        private const double MinItemScore = 0.64;
        private const string OcpApimSubscriptionKey = "Ocp-Apim-Subscription-Key";
        private const string WeatherLocationEntityName = "Weather.Location";
        private const string DateTimeEntityName = "builtin.datetimeV2.date";

        public PhraseService(IJsonConvertWrapper jsonConvert, IHttpClient httpClient, LuisSettings luisSettings)
        {
            _jsonConvert = jsonConvert;
            _httpClient = httpClient;
            _luisSettings = luisSettings;
        }

        public async Task<PhraseResult> MakeRequest(string query)
        {
            var responseContent = await MakeRequestToLuis(query);

            var result = _jsonConvert.DeserializeObject<RootObject>(responseContent);

            string location = null;
            var date = DateTime.Now;
            foreach (var item in result.Entities)
            {
                if (item.Type == WeatherLocationEntityName && item.Score > MinItemScore)
                {
                    location = item.EntityType;
                }

                if (item.Type == DateTimeEntityName)
                {
                    date = DateTime.Parse(item.Resolution.Values[0].Value);
                }
            }
            var phraseResult = new PhraseResult
            {
                Location = location,
                Date = date,
                TopIntent = result.TopScoringIntent.Intent,
                Score = result.TopScoringIntent.Score
            };

            return await Task.FromResult(phraseResult);
        }

        private async Task<string> MakeRequestToLuis(string query)
        {
            var queryString = BuildQueryString(query);

            _httpClient.AddHeaders(OcpApimSubscriptionKey, _luisSettings.SubscriptionKey);

            var uri = _luisSettings.LuisUrl + _luisSettings.LuisAppId + "?" + queryString;
            var response = await _httpClient.GetAsync(uri);

            var responseContent = await response.Content.ReadAsStringAsync();

            return responseContent;
        }

        private NameValueCollection BuildQueryString(string query)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString["q"] = query;

            queryString["timezoneOffset"] = "0";
            queryString["verbose"] = "false";
            queryString["spellCheck"] = "false";
            queryString["staging"] = "false";

            return queryString;
        }
    }
}
