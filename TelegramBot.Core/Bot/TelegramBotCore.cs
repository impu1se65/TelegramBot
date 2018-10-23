using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Core.Interfaces;
using TelegramBot.Infrastucture.Contracts;
using TelegramBot.Infrastucture.Interfaces;

namespace TelegramBot.Core.Bot
{
    public class TelegramBotCore : ITelegramBotCore
    {
        private readonly IWeatherPhraseFacade _weatherPhraseFacade;
        private readonly ITelegramBotClient _client;
        private readonly ILogger _logger;
        private readonly User _user;
        private string _previousMessage = string.Empty;
        private const string UserInput = "User message: ";
        private const string BotResponse = "Bot response: ";
        private const string IndicateStartPhrase = "Hey bot,";
        private const string DontUnderstandError = "I am dont understand, please repeat valid weather request. Sorry," +
                                                   "but i understand only weather requests";

        public TelegramBotCore(
            IWeatherPhraseFacade weatherPhraseFacade, 
            TelegramBotToken token, 
            ITelegramBotClientFactory factory, 
            ILogger<TelegramBotCore> logger)
        {
            _weatherPhraseFacade = weatherPhraseFacade;
            _logger = logger;
            _client = factory.GeTelegramBotClient(token.Token);
            _user = _client.GetMeAsync().Result;
            SubscribeToEvents();
        }

        public void StartReceiving()
        {
            _logger.LogInformation("App start");
            _logger.LogInformation($"Start listening for @{_user.Username}");
            _client.StartReceiving();
        }

        public void StopReceiving()
        {
            _client.StopReceiving();
        }

        private void SubscribeToEvents()
        {
            _client.OnMessage += BotOnMessageReceived;
            _client.OnMessageEdited += BotOnMessageReceived;

            _client.OnReceiveError += BotOnReceiveError;
        }

        private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            if (message == null || message.Type != MessageType.Text) return;

            await _client.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

            LogMessages(message.Text, UserInput);
            if (message.Text.StartsWith(IndicateStartPhrase))
            {
                _previousMessage = message.Text;

                var result = await _weatherPhraseFacade.GetForecast(message.Text, message.From.Username);
                LogMessages(result, BotResponse);

                await _client.SendTextMessageAsync(message.Chat.Id, result);
            }
            else
            {
                LogMessages(DontUnderstandError, BotResponse);

                await _client.SendTextMessageAsync(message.Chat.Id, DontUnderstandError);
            }
        }

        private void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
           _logger.LogError(
               $"Received error: { receiveErrorEventArgs.ApiRequestException.ErrorCode} -" +
               $" { receiveErrorEventArgs.ApiRequestException.Message}");
        }

        private void LogMessages(string message, string input)
        {
            _logger.LogInformation($"{input}: {message}");
        }

    }
}
