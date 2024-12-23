using System;
using System.Collections.Generic;
using Domain.Models;

namespace Domain.Containers
{
 

        public class UserNameAndID
        {
            public string UserID { get; set;}
            public string UserName { get; set;}
        }
    public class GameRoom
    {
        public string ID { get; set; }
        public List<UserNameAndID?> IDOfClients { get; set; } = new List<UserNameAndID?>();
        public List<BetData> Bets { get; set; } = new List<BetData>();

        // События
        public event EventHandler<UserNameAndID> ClientEntered;
        public event EventHandler<string> ClientExited;
        public event EventHandler<BetData> BetTaken;
        public event EventHandler<string> RoundEnded;

        // Метод для вызова события, когда клиент заходит
        public void TriggerClientEntered(UserNameAndID clientId)
        {
            ClientEntered?.Invoke(this, clientId);
        }

        // Метод для вызова события, когда клиент выходит
        public void TriggerClientExited(string clientId)
        {
            ClientExited?.Invoke(this, clientId);
        }

        // Метод для вызова события, когда ставка принимается
        public void TriggerBetTaken(BetData betData)
        {
            BetTaken?.Invoke(this, betData);
        }

        // Метод для вызова события, когда раунд завершается
        public void TriggerRoundEnded(string message)
        {
            RoundEnded?.Invoke(this, message);
        }
    }
}
