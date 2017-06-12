using System;
using System.Collections.Generic;
using LigaManagerServer.Contracts;
using LigaManagerServer.Interfaces;
using LigaManagerServer.Models;

namespace LigaManagerServer.Services
{
    public class LigaManagerService : ILigaManagerService
    {
        private static readonly object StaticLock = new object();
        private readonly IPersistenceService<Bettor> _bettorPersistenceService = new PersistenceService<Bettor>();
        private readonly IPersistenceService<Bet> _betPersistenceService = new PersistenceService<Bet>();
        private readonly IPersistenceService<Season> _seasonPersistenceService = new PersistenceService<Season>();
        private readonly IPersistenceService<Match> _matchPersistenceService = new PersistenceService<Match>();
        public List<Match> GetMatches(Season season)
        {
            lock (StaticLock)
            {
                return _matchPersistenceService.GetAll();
            }
        }

        public List<Bet> GetBets(Bettor bettor)
        {
            lock (StaticLock)
            {
                var bets = _betPersistenceService.GetAll();
                var findAll = bets.FindAll(x => x.Bettor.Equals(bettor));

                return findAll;
            }
        }

        public List<Bettor> GetBettors()
        {
            lock (StaticLock)
            {
                var bettors = _bettorPersistenceService.GetAll();
                return bettors;
            }
        }
    }
}