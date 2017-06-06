using System.Collections.Generic;
using LigaManagerServer.Framework;
using LigaManagerServer.Interfaces;
using LigaManagerServer.Models;

namespace LigaManagerServer.Services
{
    public class BetService : IBetService
    {
        private readonly Repository<Bet> _repository = new Repository<Bet>();
        private static readonly object StaticLock = new object();
        public List<Bet> GetBets()
        {
            lock (StaticLock)
            {
                var bets = _repository.GetAll();
                return bets;
            }
        }

        public bool AddBet(Bet bet)
        {
            lock (StaticLock)
            {
                _repository.Save(bet);
                return true;
            }
        }

        public bool DeleteBet(Bet bet)
        {
            lock (StaticLock)
            {
                _repository.Delete(bet);
                return true;
            }
        }

        public bool ChangeBet(Bet bet)
        {
            lock (StaticLock)
            {
                return true;
            }
        }
    }
}