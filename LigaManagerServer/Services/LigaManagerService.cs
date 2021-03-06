﻿using System.Collections.Generic;
using LigaManagerServer.Contracts;
using LigaManagerServer.Interfaces;
using LigaManagerServer.Models;
using static LigaManagerServer.Lock.Lock;

namespace LigaManagerServer.Services
{
    public class LigaManagerService : ILigaManagerService
    {
        private readonly IPersistenceService<Bettor> _bettorPersistenceService = new PersistenceService<Bettor>();
        private readonly IPersistenceService<Bet> _betPersistenceService = new PersistenceService<Bet>();
        private readonly IPersistenceService<Season> _seasonPersistenceService = new PersistenceService<Season>();
        private readonly IPersistenceService<Match> _matchPersistenceService = new PersistenceService<Match>();
        private readonly IPersistenceService<Team> _teamPersistenceService = new PersistenceService<Team>();
        private readonly IPersistenceService<SeasonToTeamRelation> _seasonToTeamRelationService = new PersistenceService<SeasonToTeamRelation>();

        public bool IsOpen()
        {
            lock (StaticLock)
            {
                return true;
            }
        }

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

        public List<SeasonToTeamRelation> GetSeasonToTeamRelation(Season season)
        {
            lock (StaticLock)
            {
                var seasonToTeamRelations = _seasonToTeamRelationService.GetAll();
                var teamsOfSeason = seasonToTeamRelations.FindAll(x => x.Season.Equals(season));    
                return teamsOfSeason;
            }
        }

        public List<SeasonToTeamRelation> GetAllSeasonToTeamRelation()
        {
            lock (StaticLock)
            {
                var teams = _seasonToTeamRelationService.GetAll();
                return teams;
            }
        }

        public List<Team> GetAllTeams()
        {
            lock (StaticLock)
            {
                var teams = _teamPersistenceService.GetAll();
                return teams;
            }
        }

    }
}