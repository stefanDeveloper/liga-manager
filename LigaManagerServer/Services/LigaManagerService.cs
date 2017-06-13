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
        private readonly IPersistenceService<SeasonToTeamRelation> _seasonToTeamRelationService = new PersistenceService<SeasonToTeamRelation>();

        public List<Match> GetMatches(Season season)
        {
            lock (StaticLock)
            {
                var matches = _matchPersistenceService.GetAll();
                var matchesOfSeason = matches.FindAll(x => x.Season.Equals(season));
                return matchesOfSeason;
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

        public List<Bet> GetAllBets()
        {
            lock (StaticLock)
            {
                var bets = _betPersistenceService.GetAll();
                return bets;
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

        public Bettor GetBettor(string nickname)
        {
            lock (StaticLock)
            {
                var bettors = _bettorPersistenceService.GetAll();
                var bettor = bettors.Find(x => x.Nickname.ToUpper().Equals(nickname.ToUpper()));
                return bettor;
            }
        }

        public List<Season> GetSeasons()
        {
            lock (StaticLock)
            {
                var seasons = _seasonPersistenceService.GetAll();
                return seasons;
            }
        }

        public List<SeasonToTeamRelation> GetTeams(Season season)
        {
            lock (StaticLock)
            {
                var seasonToTeamRelations = _seasonToTeamRelationService.GetAll();
                var teamsOfSeason = seasonToTeamRelations.FindAll(x => x.Season.Equals(season));    
                return teamsOfSeason;
            }
        }

    }
}