using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using Domain.Containers;
using Domain.Interfaces.Infrastructure.JsonDeserializer;
using Domain.Response;

namespace CasinoApi.Router
{
    public class RoomEventsWS
    {
        private readonly Dictionary<WebSocket, Dictionary<string, Delegate>> _clientHandlers = new();
        private readonly IJsonConverter _jsonConverter;

        public RoomEventsWS(IJsonConverter jsonConverter)
        {
            _jsonConverter = jsonConverter;
        }

        public void RoomJoin(GameRoom room, WebSocket webSocket)
        {
            if (!_clientHandlers.ContainsKey(webSocket))
            {
                _clientHandlers[webSocket] = new Dictionary<string, Delegate>();
            }
            Console.WriteLine(_clientHandlers.Count);

            // Добавляем обработчики для событий
            AddHandler(room, webSocket, "ClientEntered", new EventHandler<UserNameAndID>((sender, clientId) => HandleEvent(sender, new EventResult<UserNameAndID>() { Event = "ClientEntered", Data = clientId }, webSocket)));
            AddHandler(room, webSocket, "ClientExited", new EventHandler<string>((sender, clientId) => HandleEvent(sender, new EventResult<string>() { Event = "ClientExited", Data = clientId }, webSocket)));
            AddHandler(room, webSocket, "BetTaken", new EventHandler<BetData>((sender, betData) => HandleEvent(sender, new EventResult<BetData>() { Event = "BetTaken", Data = betData }, webSocket)));
            AddHandler(room, webSocket, "RoundEnded", new EventHandler<string>((sender, message) => HandleEvent(sender, new EventResult<string>() { Event = "RoundEnded", Data = message }, webSocket)));
        }

        public void RoomLeft(GameRoom room, WebSocket webSocket)
        {
            if (_clientHandlers.TryGetValue(webSocket, out var handlers))
            {
                foreach (var (eventName, handler) in handlers)
                {
                    RemoveHandler(room, eventName, handler);
                }
                _clientHandlers.Remove(webSocket);
            }
        }

        private void AddHandler(GameRoom room, WebSocket webSocket, string eventName, Delegate handler)
        {
            switch (eventName)
            {
                case "ClientEntered":
                    room.ClientEntered += (EventHandler<UserNameAndID>)handler;
                    break;
                case "ClientExited":
                    room.ClientExited += (EventHandler<string>)handler;
                    break;
                case "BetTaken":
                    room.BetTaken += (EventHandler<BetData>)handler;
                    break;
                case "RoundEnded":
                    room.RoundEnded += (EventHandler<string>)handler;
                    break;
            }

            _clientHandlers[webSocket][eventName] = handler;
        }

        private void RemoveHandler(GameRoom room, string eventName, Delegate handler)
        {
            switch (eventName)
            {
                case "ClientEntered":
                    room.ClientEntered -= (EventHandler<UserNameAndID>)handler;
                    break;
                case "ClientExited":
                    room.ClientExited -= (EventHandler<string>)handler;
                    break;
                case "BetTaken":
                    room.BetTaken -= (EventHandler<BetData>)handler;
                    break;
                case "RoundEnded":
                    room.RoundEnded -= (EventHandler<string>)handler;
                    break;
            }
        }

        private async void HandleEvent<T>(object sender, T message, WebSocket webSocket) where T : class
        {
            Console.WriteLine("ПОПЫТКА ОТПРАВИТ СООБЩЕНИЕ");
            if (webSocket.State == WebSocketState.Open)
            {
                var buffer = Encoding.UTF8.GetBytes(_jsonConverter.ToJson(new Response<T>().Success(message)));
                await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}
