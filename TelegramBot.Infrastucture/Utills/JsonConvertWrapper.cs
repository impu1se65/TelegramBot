using Newtonsoft.Json;
using TelegramBot.Infrastucture.Interfaces;

namespace TelegramBot.Infrastucture.Utills
{
    public class JsonConvertWrapper : IJsonConvertWrapper
    {
        public string SerializeObject(object value)
        {
            var json = JsonConvert.SerializeObject(value);

            return json;
        }

        public T DeserializeObject<T>(string json)
        {
            var result = JsonConvert.DeserializeObject<T>(json);

            return result;
        }
    }
}
