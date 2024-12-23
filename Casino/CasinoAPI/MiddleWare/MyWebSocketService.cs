using System.Net.WebSockets;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using CasinoApi.Containers;
using Router;
using CasinoApi.Router;
using Domain.Models;
using Domain.Containers;
using Domain.Interfaces.Response;
using Domain.Interfaces.Infrastructure.JsonDeserializer;
using BusinessLogic.Service;

public interface IMyWebSocketService
{
    Task HandleWebSocketAsync(WebSocket webSocket, string ID);
}

public class MyWebSocketService : IMyWebSocketService
{
    private readonly List<CoupleWsId> _webSockets = new List<CoupleWsId>();
    private readonly CasinoApi.Router.IRouter _router;
    private readonly RoomEventsWS _roomJoinWS;
    private readonly IJsonConverter _jsonConverter;
      private readonly GameRooms _gameRooms;

    public MyWebSocketService(CasinoApi.Router.IRouter router, RoomEventsWS roomJoinWS,IJsonConverter jsonConverter, GameRooms gameRoom)
    {
        _router = router;
        _roomJoinWS = roomJoinWS;
        _jsonConverter = jsonConverter; 
        _gameRooms = gameRoom;
    }
    
    public async Task HandleWebSocketAsync(WebSocket webSocket, string Id)
    {

        var buffer = new byte[1024 * 4]; 
        string greetingMessage = "Hello from server!";
    
        var bytes = Encoding.UTF8.GetBytes(greetingMessage);
        await webSocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);

        var coupleWsId = new CoupleWsId() { webSocket = webSocket, ID = Id };
        _webSockets.Add(coupleWsId);
        


        try
        {
            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                GameRoom gameRoom = null; // Инициализируем переменную для игры

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Console.WriteLine("Received message: " + message);

                    // Получаем ответ от роутера
                    IResponse<RouterResult> ff = await _router.Route(message, Id);
                    if(ff.IsSuccess){

                        if (ff.Data.Data is GameRoom gameRoomData)
                        {

                            gameRoom = gameRoomData;
                            if(ff.Data.Event == "join")
                            {

                                Console.WriteLine(gameRoom.ID + " ID КОмнаты");

                                // Обрабатываем клиентов в игре
                                            _roomJoinWS.RoomJoin(gameRoom, webSocket);
                                foreach (var el in _webSockets)
                                {
                                    foreach (var client in gameRoom.IDOfClients)
                                    {
                                        if (el.ID == client.UserID)
                                        {
                                            
                                        }
                                    }
                                }
                            }
                            if(ff.Data.Event == "leave")
                            {
                                _roomJoinWS.RoomLeft(gameRoom, webSocket);
                            }
                        }
                       
                    }
                        message = _jsonConverter.ToJson<IResponse<RouterResult>>(ff);
                        buffer = Encoding.UTF8.GetBytes(message);
                        await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                    
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    
                        var GameRoom = _gameRooms.FindRoomByUserID(Id);
                        if(GameRoom != null){
                            _roomJoinWS.RoomLeft(GameRoom, webSocket);
                            _gameRooms.Leave(Id);
                         

                        }
                   

                   


                    Console.WriteLine("Client initiated close.");
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred: {ex.Message}");
        }
        finally
        {
            _webSockets.Remove(coupleWsId);
            Console.WriteLine($"WebSocket disconnected. Remaining connections: {_webSockets.Count}");
        }
    }
}
