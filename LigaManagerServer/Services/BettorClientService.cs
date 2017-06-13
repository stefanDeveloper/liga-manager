using System;
using System.Linq;
using LigaManagerServer.Contracts;
using LigaManagerServer.Interfaces;
using LigaManagerServer.Models;

namespace LigaManagerServer.Services
{
    public class BettorClientService : LigaManagerService, IBettorClientService
    {
        private static readonly object StaticLock = new object();
        private readonly IPersistenceService<Bettor> _bettorPersistenceService = new PersistenceService<Bettor>();
        private readonly IPersistenceService<Bet> _betPersistenceService = new PersistenceService<Bet>();
        private readonly IPersistenceService<Season> _seasonPersistenceService = new PersistenceService<Season>();
        private readonly IPersistenceService<Match> _matchPersistenceService = new PersistenceService<Match>();
        private readonly IPersistenceService<SeasonToTeamRelation> _seasonToTeamRelationService = new PersistenceService<SeasonToTeamRelation>();
        public bool IsValidNickname(string name)
        {
            lock (StaticLock)
            {
                var bettors = _bettorPersistenceService.GetAll();
                var bettor = bettors.Find(x => x.Nickname.ToUpper().Equals(name.ToUpper()));
                return bettor != null;
            }
        }

        public bool AddBet(Bet bet)
        {
            lock (StaticLock)
            {

               return _betPersistenceService.Add(bet);
            }
        }

        public bool ChangeBet(Bet bet)
        {
            lock (StaticLock)
            {
                if (!(DateTime.Now.AddMinutes(30) < bet.Match.DateTime))
                    return false;
                return _betPersistenceService.Update(bet);
            }
        }

        public Bet GetBet(Match match, Bettor bettor)
        {
            lock (StaticLock)
            {
                var bets = _betPersistenceService.GetAll();
                var betsOfMatch = bets.FindAll(x => x.Bettor.Equals(bettor) && x.Match.Equals(match));
                return betsOfMatch.Count != 1 ? null : betsOfMatch.First();
            }
        }


    }
}