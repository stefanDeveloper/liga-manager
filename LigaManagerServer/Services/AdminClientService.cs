using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Conventions;
using LigaManagerServer.Contracts;
using LigaManagerServer.Interfaces;
using LigaManagerServer.Models;
using NHibernate.Event;
using NHibernate.Util;

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
                // if the user has any current bets, all of them will be deleted
                findAll.ForEach(x => _betPersistenceService.Delete(x));
                // finally the user will be deleted
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
                // deleted all matches of a team
                var matches = _matchPersistenceService.GetAll();
                var matchesOfTeam = matches.FindAll(x => x.HomeTeam.Equals(team) || x.AwayTeam.Equals(team));
                matchesOfTeam.ForEach(x => _matchPersistenceService.Delete(x));

                //delete all relations to seasons
                var seasonToTeamRelations = _seasonToTeamRelationService.GetAll();
                var seasonsOfTeam = seasonToTeamRelations.FindAll(x => x.Team.Equals(team));
                seasonsOfTeam.ForEach(x => _seasonToTeamRelationService.Delete(x));

                // delete  team
                var isSuccess = _teamPersistenceService.Delete(team);
                if (!isSuccess) return false;

                return true;
            }
        }

        public bool DeleteSeason(Season season)
        {
            lock (StaticLock)
            {
                // delete all SeasonToTeamRelations
                var seasonToTeamRelations = _seasonToTeamRelationService.GetAll();
                var teamsOfSeason = seasonToTeamRelations.FindAll(x => x.Season.Equals(season));
                teamsOfSeason.ForEach(x => _seasonToTeamRelationService.Delete(x));

                // delete all matches of a season
                var matches = _matchPersistenceService.GetAll();
                var matchesOfSeason = matches.FindAll(x => x.Season.Equals(season));
                matchesOfSeason.ForEach(x => _matchPersistenceService.Delete(x));

                // delete season
                var isSuccess = _seasonPersistenceService.Delete(season);
                return isSuccess;
            }
        }

        public bool AddSeason(Season season)
        {
            lock (StaticLock)
            {
                var seasons = _seasonPersistenceService.GetAll();
                var max = seasons.Max(x => x.Sequence);
                season.Sequence = max + 1;
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

        public bool DeleteSeasonToTeamRelation(SeasonToTeamRelation seasonToTeamRelation)
        {
            lock (StaticLock)
            {
                return _seasonToTeamRelationService.Delete(seasonToTeamRelation);
            }
        }

        public bool AddSeasonToTeamRelation(SeasonToTeamRelation seasonToTeamRelation)
        {
            lock (StaticLock)
            {
                return _seasonToTeamRelationService.Add(seasonToTeamRelation);
            }
        }

        public void GenerateMatches(Season season, DateTime beginDateTime, DateTime endDateTime)
        {
            lock (StaticLock)
            {
                var seasonToTeamRelations = _seasonToTeamRelationService.GetAll();
                var teamsOfSeason = seasonToTeamRelations.FindAll(x => x.Season.Equals(season));
                
                var dateTimes = CreateDateTimes(beginDateTime, endDateTime, new List<DateTime>());
                var matchesOfSeason = new List<Match>();
                foreach (var seasonToTeamRelation in teamsOfSeason)
                {
                    foreach (var teams in teamsOfSeason)
                    {
                        if (seasonToTeamRelation.Equals(teams)) continue;
                        var match = new Match
                        {
                            HomeTeam = seasonToTeamRelation.Team,
                            AwayTeam = teams.Team,
                            Season = seasonToTeamRelation.Season,
                        };
                        
                        matchesOfSeason.Add(match);
                    }
                }
                var result = GenerateMatches(new List<Match>(), matchesOfSeason, dateTimes, 1);
            }
        }

        private List<Match> GenerateMatches(List<Match> result, List<Match> matches, List<DateTime> dayOfWeeks, int matchDay)
        {
            if (!matches.IsEmpty())
            {
                foreach (var dateTime in dayOfWeeks)
                {
                    foreach (var match in matches)
                    {
                        if (dateTime.DayOfWeek == DayOfWeek.Friday)
                        {
                            var currentMatch = match;
                            matches.Remove(match);
                            currentMatch.DateTime = dateTime;
                            currentMatch.MatchDay = matchDay;
                            result.Add(currentMatch);
                            // remove from group
                            dayOfWeeks.Remove(dateTime);
                            return GenerateMatches(result, matches, dayOfWeeks, matchDay);

                        }
                        else if (dateTime.DayOfWeek == DayOfWeek.Sunday)
                        {
                            var currentMatch = match;
                            matches.Remove(match);
                            currentMatch.DateTime = dateTime;
                            currentMatch.MatchDay = matchDay;
                            result.Add(currentMatch);
                            dayOfWeeks.Remove(dateTime);
                            return GenerateMatches(result, matches, dayOfWeeks, dateTime.Hour == 17 ? ++matchDay : matchDay);
                        }
                    }
                }
                return result;
            }
            else
            {
                return result;
            }

        }

        /// <summary>
        /// Generates all validate DateTimes during the TimeSpan of the season.
        /// </summary>
        /// <param name="beginn"></param>
        /// <param name="end"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private List<DateTime> CreateDateTimes(DateTime beginn, DateTime end, List<DateTime> result)
        {
            if (!beginn.Equals(end))
            {
                if (DayOfWeek.Friday == beginn.DayOfWeek)
                {
                    result.Add(new DateTime(beginn.Year, beginn.Month, beginn.Day, 20,30,0));
                }
                else if (DayOfWeek.Sunday == beginn.DayOfWeek)
                {
                    result.Add(new DateTime(beginn.Year, beginn.Month, beginn.Day, 15, 30, 0));
                    result.Add(new DateTime(beginn.Year, beginn.Month, beginn.Day, 17, 30, 0));
                }
                else if (DayOfWeek.Saturday == beginn.DayOfWeek)
                {
                    result.Add(new DateTime(beginn.Year, beginn.Month, beginn.Day, 15, 30, 0));
                }

                return CreateDateTimes(beginn.AddDays(1), end, result);
            }
            else
            {
                return result;
            }

        }
    }
}