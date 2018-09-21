using System.Threading.Tasks;
using TelegramBot.Infrastucture.Contracts;

namespace TelegramBot.Infrastucture.Interfaces
{
    public interface IPhraseService
    {
        Task<PhraseResult> MakeRequest(string query);
    }
}