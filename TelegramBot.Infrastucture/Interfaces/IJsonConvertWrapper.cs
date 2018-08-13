namespace TelegramBot.Infrastucture.Interfaces
{
    public interface IJsonConvertWrapper
    {
        string SerializeObject(object value);
        T DeserializeObject<T>(string json);
    }
}