using LigaManagerServer.Contracts;
using LigaManagerServer.Interfaces;
using LigaManagerServer.Models;

namespace LigaManagerServer.Services
{
    public class AdminClientService : LigaManagerService, IAdminClientService
    {
        private static readonly object StaticLock = new object();
        private readonly IPersistenceService<Bettor> _bettorPersistenceService = new PersistenceService<Bettor>();
        private readonly IPersistenceService<Team> _teamPersistenceService = new PersistenceService<Team>();
        private readonly IPersistenceService<Bet> _betPersistenceService = new PersistenceService<Bet>();
        private readonly IPersistenceService<Season> _seasonPersistenceService = new PersistenceService<Season>();
        private readonly IPersistenceService<Match> _matchPersistenceService = new PersistenceService<Match>();
        private readonly IPersistenceService<SeasonToTeamRelation> _seasonToTeamRelationService = new PersistenceService<SeasonToTeamRelation>();

        public bool AddBettor(Bettor bettor)
        {
            lock (StaticLock)
            {
                return _bettorPersistenceService.Add(bettor);
            }
        }

        public bool UpdateBettor(Bettor bettor)
        {
            lock (StaticLock)
            {
                return _bettorPersistenceService.Update(bettor);
            }
        }

        public bool DeleteBettor(Bettor bettor)
        {
            lock (StaticLock)
            {
                var bets = _betPersistenceService.GetAll();
                var findAll = bets.FindAll(x => x.Bettor.Equals(bettor));
                if (findAll.Count > 0) return false;
                return _bettorPersistenceService.Delete(bettor);
            }
        }

        public bool AddTeam(Team team)
        {
            lock (StaticLock)
            {
                return _teamPersistenceService.Add(team);
            }
        }

        public bool UpdateTeam(Team team)
        {
            lock (StaticLock)
            {
                return _teamPersistenceService.Update(team);
            }
        }

        public bool DeleteTeam(Team team)
        {
            lock (StaticLock)
            {
                var isSuccess = _teamPersistenceService.Delete(team);
                if (!isSuccess) return false;
                var matches = _matchPersistenceService.GetAll();
                matches.ForEach(x =>
                {
                    if (x.HomeTeam.Equals(team))
                    {
                        _matchPersistenceService.Delete(x);
                    }

                    if (x.AwayTeam.Equals(team))
                    {
                        _matchPersistenceService.Delete(x);
                    }
                });
                return true;
            }
        }

        public bool DeleteSeason(Season season)
        {
            lock (StaticLock)
            {
                var seasonToTeamRelations = _seasonToTeamRelationService.GetAll();
                var teamsOfSeason = seasonToTeamRelations.FindAll(x => x.Season.Equals(season));
                if (teamsOfSeason.Count > 0) return false;

                var matches = _matchPersistenceService.GetAll();
                var matchesOfSeason = matches.FindAll(x => x.Season.Equals(season));
                if (matchesOfSeason.Count > 0) return false;

                var isSuccess = _seasonPersistenceService.Delete(season);
                return isSuccess;
            }
        }

        public bool AddSeason(Season season)
        {
            lock (StaticLock)
            {
                return _seasonPersistenceService.Add(season);
            }
        }

        public bool UpdateSeason(Season season)
        {
            lock (StaticLock)
            {
                return _seasonPersistenceService.Update(season);
            }
        }

        public bool DeleteMatch(Match match)
        {
            lock (StaticLock)
            {
                return _matchPersistenceService.Delete(match);
            }
        }

        public bool AddMatch(Match match)
        {
            lock (StaticLock)
            {
                return _matchPersistenceService.Add(match);
            }
        }

        public bool UpdateMatch(Match match)
        {
            lock (StaticLock)
            {
                return _matchPersistenceService.Update(match);
            }
        }

        public void GenerateMatches()
        {
            //TODO Generate Matches for an Season
            throw new System.NotImplementedException();
        }
    }
}