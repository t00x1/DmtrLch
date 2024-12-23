using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Wrapper;
using Domain.Containers;
using Domain.Models;

namespace BusinessLogic.Service
{   
    public class GameRooms
    {
        private readonly RepositoryWrapperServer _repositoryWrapperServer;
        private readonly List<GameRoom> _rooms = new();
        public GameRooms(RepositoryWrapperServer repositoryWrapperServer)
        {
            _repositoryWrapperServer = repositoryWrapperServer;
        }
        public GameRoom? CreateRoom(string id)
        {
            if (_rooms.Any(room => room.ID == id))
            {
                return null;
            }

            var room = new GameRoom { ID = id };
            _rooms.Add(room);
            return room;
        }
        public async Task<bool> TakeBet(string id,string username, int bet, string betOn )
        {
            BetData Bet = new BetData(){UserID = id, UserName = username, Bet = bet, BetOn = betOn}; 
            var room = FindRoomByUserID(id);
            if(room == null){
                Console.WriteLine("Комната не найден");
                return false;
            }
            if(room.Bets.FirstOrDefault(X=> X.UserID == id) == null){
                 Console.WriteLine("Пользователь уже сделал ставку" + id);
            }
            var IsEnough = await _repositoryWrapperServer.Balance.FindByCondition(X =>X.IdOfUser == id && X.Balance1 >=  bet );
            if(IsEnough.Any()){
                room.Bets.Add(Bet);
                room.TriggerBetTaken(Bet);
                return true;
            }
                Console.WriteLine("Недостаточный баланс " + id);
            return false;
        }

        public GameRoom? Join(string roomID, string userID, string userName)
        {
            foreach(var el in _rooms){
                if(el.ID == roomID)
                {
                    foreach(var elem in el.IDOfClients)
                    {
                        Console.ForegroundColor = ConsoleColor.Red; 
                        Console.WriteLine(elem.UserName);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(userName);
                        Console.ResetColor(); 
                        
                    }
                }
            }
            if (_rooms.Any(room => room.IDOfClients.Any(client => client?.UserID == userID)))
            {
                Console.WriteLine($"Пользователь уже находится в какой-то комнтае {userName}" );
                return null;
            }

            var room = _rooms.FirstOrDefault(room => room.ID == roomID);
            if (room != null)
            {
                room.TriggerClientEntered(new UserNameAndID(){UserID = userID,UserName = userName});
                 Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Пользователь " + userName + "Добавлен \n" + userID );
                        Console.ResetColor(); 
                room.IDOfClients.Add(new UserNameAndID { UserID = userID, UserName = userName });
                return room;
            }

            Console.WriteLine("Комната не найдена");
            return null;
        }

        public GameRoom? Leave(string userID)
        {
            foreach (var room in _rooms)
            {
                var client = room.IDOfClients.FirstOrDefault(client => client?.UserID == userID);
                if (client != null)
                {
                    room.TriggerClientExited(userID);
                    room.IDOfClients.Remove(client);
                    return room;
                }
            }
            return null;
        }

        public bool Leave(GameRoom room, string userID)
        {
            var client = room.IDOfClients.FirstOrDefault(el => el.UserID == userID);

            if (client != null)
            {
                room.IDOfClients.Remove(client);
                return true;
            }

            return false;
        }

        public GameRoom? FindRoomByUserID(string userID)
        {
            foreach (var room in _rooms)
            {
                var client = room.IDOfClients.FirstOrDefault(client => client?.UserID == userID);
                if (client != null)
                {
                    return room;
                }
            }
            return null;
        }

        public bool UserExists(string roomID, string userID)
        {
            var room = _rooms.FirstOrDefault(room => room.ID == roomID);
            return room?.IDOfClients.Any(client => client?.UserID == userID) ?? false;
        }

        public List<UserNameAndID?>? GetUsersOfRoom(string roomID)
        {
            var room = _rooms.FirstOrDefault(room => room.ID == roomID);
            return room?.IDOfClients;
        }

        public bool MarkUserLeft(string roomID, string userID)
        {
            var room = _rooms.FirstOrDefault(room => room.ID == roomID);
            if (room != null)
            {
                for (int i = 0; i < room.IDOfClients.Count; i++)
                {
                    if (room.IDOfClients[i]?.UserID == userID)
                    {
                        room.IDOfClients[i] = null;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
