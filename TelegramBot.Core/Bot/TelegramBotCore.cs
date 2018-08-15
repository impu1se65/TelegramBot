using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
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
        private readonly TelegramBotToken _token;
        private readonly ITelegramBotClient _client;
        private readonly ILogger _logger;
        private readonly User _user;
        private string _previousMessage = string.Empty;
        private const string IndicateStartPhrase = "Hey bot";

        public TelegramBotCore(
            IWeatherPhraseFacade weatherPhraseFacade, 
            TelegramBotToken token, 
            ITelegramBotClientFactory factory, 
            ILogger<TelegramBotCore> logger)
        {
            _weatherPhraseFacade = weatherPhraseFacade;
            _token = token;
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

        private  async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            if (message == null || message.Type != MessageType.Text) return;


            if (message.Text.StartsWith(IndicateStartPhrase))
            {
                _previousMessage = message.Text;
                var result = await _weatherPhraseFacade.GetForecast(message.Text, _user.Username);

                await _client.SendTextMessageAsync(message.Chat.Id, result);
            }
        }

        private void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
           _logger.LogError(
               $"Received error: { receiveErrorEventArgs.ApiRequestException.ErrorCode} -" +
               $" { receiveErrorEventArgs.ApiRequestException.Message}");
        }
    }
}
