using CasinoApi.Router;
using Domain.Interfaces.Infrastructure.JsonDeserializer;
using Infrastructure.Services;
using Domain.Containers;
using Domain.Models;
using BusinessLogic.Service;
using Domain.Interfaces.Response;
using Domain.Response;
using DataAccess.Wrapper;


namespace Router 
{
    public class Router : CasinoApi.Router.IRouter
    {
        private readonly IJsonConverter _jsonConverter;
        private readonly GameRooms _gameRooms;
        private readonly GameWheel _gameWheel;
        private readonly RepositoryWrapperServer _repositoryWrapperServer;



        // Конструктор для инжекции зависимостей
        public Router(IJsonConverter jsonConverter, GameRooms gameRooms, GameWheel gameWheel, RepositoryWrapperServer repositoryWrapperServer)
        {
            _jsonConverter = jsonConverter;
            _gameRooms = gameRooms;
           _gameWheel = gameWheel;
           _repositoryWrapperServer = repositoryWrapperServer;
        }

        public async Task<IResponse<RouterResult>> Route(string jsonParam, string IdOfUser)
        {
            // Десериализация JSON-строки в объект QueryData
            QueryData Data = _jsonConverter.FromJson<QueryData>(jsonParam);

            // Разбиение события на компоненты
            string[] domens = Data.Event.ToLower().Split('/');
            GameRoom? gameRoom;
            if (domens.Length > 0)
                switch (domens[0])
                {
                    case "room":
                        Console.WriteLine("норм");
                        {
                            switch (domens[1])
                            {
                                case "join":

                                    var users = await _repositoryWrapperServer.User.FindByCondition(X=> X.IdOfUser == IdOfUser);
                                    //ПОТОМ ПЕРЕДЕЛАТЬ, А ТО НЕ ОПТИМИЗИРОВАННо
                                    var user = users.FirstOrDefault();  
                                    gameRoom = _gameRooms.Join(Data.Room, IdOfUser,user.UserName);
                                    if(gameRoom == null){//абабабабабабабаа

                                    return new Response<RouterResult>().Error("Комната не найдена или че там"); 
                                    }
                                    
                                    return new Response<RouterResult>().Success(new RouterResult(){Event = "join", Data = gameRoom}); 
                                case "leave":
                                
                                
                                    gameRoom =_gameRooms.Leave(IdOfUser);
                                    return new Response<RouterResult>().Success(new RouterResult(){Event = "leave", Data = gameRoom});  
                                    
                                case "bet":
                                    if(Data.Bet >= 100  && !string.IsNullOrWhiteSpace( Data.BetOn) && _gameRooms.UserExists(Data.Room, IdOfUser)){
                                       

                                        var userss = await _repositoryWrapperServer.User.FindByCondition(X=> X.IdOfUser == IdOfUser);
                                        // //ПОТОМ ПЕРЕДЕЛАТЬ, А ТО НЕ ОПТИМИЗИРОВАННо
                                        var usersss = userss.FirstOrDefault();  
                                        if(usersss != null){
                                        if( await _gameRooms.TakeBet( IdOfUser, usersss.UserName, Data.Bet ?? 0, Data.BetOn)){

                                            return new Response<RouterResult>().Success(new RouterResult(){Event = "bet", }); 
                                        }

                                        }
                                        return new Response<RouterResult>().Error("Недостаточный баланс или ставка уже сделана");

                                    }
                                        Console.WriteLine("not success");
                                    
                                    return new Response<RouterResult>().Error("Ставка должна быть не меньше 100 фишек");
                                    
                                default:
                                    return new Response<RouterResult>().Error(); 
                            }
                        }

                    case "game":
                        switch (domens[1])
                        {
                            case "bet":
                                return new Response<RouterResult>().Error();
                            case "unbet":

                               return new Response<RouterResult>().Error();
                            default:
                                return new Response<RouterResult>().Error();
                        }

                    default:
                        Console.WriteLine("dsd");
                        return new Response<RouterResult>().Error();
                }

           return new Response<RouterResult>().Error();
        }
    }
}
