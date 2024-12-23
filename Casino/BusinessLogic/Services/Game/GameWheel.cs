using Domain.Interfaces.Repository;
using Domain.Common.Generic.Validation;
using Domain.Common.Generic;
using System.Security.Claims;
using Domain.Interfaces.Common.Generic;
using Domain.Models;
using Domain.Interfaces.Infrastructure;
using Domain.Interfaces.Infrastructure.Email;
using Domain.Interfaces.BusinessLogic.Factory.Code;
using Microsoft.Extensions.Logging;
using Domain.Interfaces.Response;
using Domain.Interfaces.Infrastructure.File;
using Domain.Response;
using System.Linq;
using Domain.Interfaces.Infrastructure.JWT;
using Domain.Containers;
using System.Linq;
using System.Threading.Tasks;
using Domain.DTO;
using System.Numerics;
using DataAccess.Wrapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class GameWheel : IAsyncDisposable
    {
        private readonly RepositoryWrapperServer _repositoryWrapper;
        private readonly GameRooms _rooms;
        private GameRoom _room;

        public GameWheel(GameRooms gameRooms, RepositoryWrapperServer wrapperFactory)
        {
            _rooms = gameRooms;
            _repositoryWrapper = wrapperFactory;
        }

        public async Task CreateGame(CancellationToken stoppingToken)
        {
            Console.WriteLine("TEST1");

            _room = _rooms.CreateRoom("Wheel");
            await _repositoryWrapper.User.FindAll();
            await _repositoryWrapper.SaveChangesAsync();
            await LoopGameEvent(stoppingToken);
        }

        public async Task LoopGameEvent(CancellationToken stoppingToken)
        {
            Random random = new Random();

            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var clientId in _room.IDOfClients)
                {
                    Console.WriteLine($"Client ID: {clientId}");
                }

                int generated = random.Next(0, 37);
                string result = generated == 0 ? "single" : (generated % 2 == 0 ? "chetn" : "nechetn");
                _room.TriggerRoundEnded(generated.ToString());

                foreach (BetData bet in _room.Bets)
                {
                    int win = CalculateWin(bet, result);


                    _repositoryWrapper.DetachAllEntities();

                    var balances = await _repositoryWrapper.Balance.FindByCondition(x => x.IdOfUser == bet.UserID);
                    
                    var balance = balances.FirstOrDefault();

                    if (balance != null)
                    {
                        balance.Balance1 += win;

                        await _repositoryWrapper.UpdateEntityAsync(balance);

                        Console.WriteLine($"Updated balance for UserID: {bet.UserID}, New Balance: {balance.Balance1}");
                    }
                    else
                    {
                        Console.WriteLine($"Balance not found for UserID: {bet.UserID}");
                    }

                    Console.WriteLine($"UserID: {bet.UserID}, Bet: {bet.Bet}, Win: {win}, Result: {result}");
                }

                _room.Bets.Clear();

                try
                {
                    await Task.Delay(30000, stoppingToken);
                }
                catch (TaskCanceledException)
                {
                    Console.WriteLine("Game loop cancelled.");
                    break;
                }
                Console.WriteLine("CHEeeee");
            }
        }

        private int CalculateWin(BetData bet, string result)
        {
            if (bet.BetOn == result)
            {
                return result == "single" ? bet.Bet * 15 : bet.Bet * 2;
            }
            return -bet.Bet;
        }

        // Реализация Dispose для освобождения ресурса
        public async ValueTask DisposeAsync()
        {
            await _repositoryWrapper.DisposeAsync(); // Использование асинхронного Dispose
            Console.WriteLine("RepositoryWrapperServer disposed.");
        }
    }
}