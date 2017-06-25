using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using FluentNHibernate.Conventions;
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
        private readonly IPersistenceService<SeasonToTeamRelation> _seasonToTeamRelationService =
            new PersistenceService<SeasonToTeamRelation>();

        public bool AddBettor(Bettor bettor)
        {
            lock (StaticLock)
            {
                var bettors = _bettorPersistenceService.GetAll();
                var searchedBettors = bettors.FindAll(x => x.Nickname.ToUpper().Equals(bettor.Nickname.ToUpper()));
                if (searchedBettors.Any()) return false;
                return _bettorPersistenceService.Add(bettor);
            }
        }

        public bool UpdateBettor(Bettor bettor)
        {
            lock (StaticLock)
            {
                var bettors = _bettorPersistenceService.GetAll();
                var searchedBettors = bettors.FindAll(x => x.Nickname.ToUpper().Equals(bettor.Nickname.ToUpper()) && x.Id != bettor.Id);
                if (searchedBettors.Any()) return false;
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
                var teams = _teamPersistenceService.GetAll();
                var searchedTeams = teams.FindAll(x => x.Name.ToUpper().Equals(team.Name.ToUpper()));
                if (searchedTeams.Any()) return false;
                return _teamPersistenceService.Add(team);
            }
        }

        public bool UpdateTeam(Team team)
        {
            lock (StaticLock)
            {
                var teams = _teamPersistenceService.GetAll();
                var searchedTeams = teams.FindAll(x => x.Name.ToUpper().Equals(team.Name.ToUpper()) && x.Id != team.Id);
                if (searchedTeams.Any()) return false;
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
                var searchedSeasons = seasons.FindAll(x => x.Name.ToUpper().Equals(season.Name.ToUpper()));
                if (searchedSeasons.Any()) return false;
                var max = seasons.Max(x => x.Sequence);
                //increase season
                season.Sequence = max + 1;
                return _seasonPersistenceService.Add(season);
            }
        }

        public bool UpdateSeason(Season season)
        {
            lock (StaticLock)
            {
                var seasons = _seasonPersistenceService.GetAll();
                var searchedSeasons = seasons.FindAll(x => x.Name.ToUpper().Equals(season.Name.ToUpper()) && x.Id != season.Id);
                if (searchedSeasons.Any()) return false;
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
                if (match.AwayTeam.Equals(match.HomeTeam)) return false;
                var matches = _matchPersistenceService.GetAll();
                var exists = matches.FindAll(x => x.Season.Equals(match.Season) && x.HomeTeam.Equals(match.HomeTeam) &&
                                                  x.AwayTeam.Equals(match.AwayTeam));
                if (exists.Any()) return false;
                return _matchPersistenceService.Add(match);
            }
        }

        public bool UpdateMatch(Match match)
        {
            lock (StaticLock)
            {
                if (match.AwayTeam.Equals(match.HomeTeam)) return false;
                var matches = _matchPersistenceService.GetAll();
                var exists = matches.FindAll(x => x.Season.Equals(match.Season) && x.HomeTeam.Equals(match.HomeTeam) &&
                                                  x.AwayTeam.Equals(match.AwayTeam) && x.Id != match.Id);
                if (exists.Any()) return false;
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

        public bool GenerateMatches(Season season, DateTime beginDateTime, DateTime endDateTime)
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
                result.Sort((x, y) => x.DateTime.CompareTo(y.DateTime));
                var matches = _matchPersistenceService.GetAll();
                var filteredResult = result.FindAll(x =>
                {
                    var finded = matches.FindAll(y => x.Season.Equals(y.Season) && x.HomeTeam.Equals(y.HomeTeam) &&
                                                      x.AwayTeam.Equals(y.AwayTeam));

                    if (finded.IsEmpty())
                    {
                        return true;
                    }
                    return false;
                });
                if (!filteredResult.Any()) return false;
                filteredResult.ForEach(x => _matchPersistenceService.Add(x));
                return true;
            }
        }

        public bool AddMatchDay(XmlElement xmlElement, Season selectedSeason, int matchday)
        {
            try
            {
                var matches = new List<Match>();
                var teams = _teamPersistenceService.GetAll();
                var nodeList = xmlElement.ChildNodes;
                foreach (XmlNode el in nodeList)
                {
                    var date = el.Attributes?["Datum"]?.Value;
                    var time = el.Attributes?["Beginn"]?.Value;
                    var dateTime = DateTime.ParseExact(date + " " + time, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                    var homeNode = el["Heim"];
                    var homeScore = homeNode?.Attributes["Tore"]?.Value;
                    var homeTeam = teams.FindAll(x => x.Name.ToUpper().Equals(homeNode?.InnerText.ToUpper()));
                    var awayNode = el["Auswaerts"];
                    var awayTeam = teams.FindAll(x => x.Name.ToUpper().Equals(awayNode?.InnerText.ToUpper()));
                    var awayScore = awayNode?.Attributes["Tore"]?.Value;
                    if (awayTeam.IsEmpty() || homeTeam.IsEmpty()) return false;
                    var match = new Match
                    {
                        DateTime = dateTime,
                        Season = selectedSeason,
                        AwayTeam = awayTeam.FirstOrDefault(),
                        HomeTeam = homeTeam.FirstOrDefault(),
                        HomeTeamScore = homeScore != null ? int.Parse(homeScore) : 0,
                        AwayTeamScore = awayScore != null ? int.Parse(awayScore) : 0,
                        MatchDay = matchday
                    };
                    matches.Add(match);
                }

                matches.ForEach(match => AddMatch(match));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// Generate all Matches of a Season, including first match and return match.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="matches"></param>
        /// <param name="dateTimes"></param>
        /// <param name="matchDay"></param>
        /// <returns></returns>
        private List<Match> GenerateMatches(List<Match> result, List<Match> matches, List<DateTime> dateTimes,
            int matchDay)
        {
            if (matches.IsEmpty()) return result;
            foreach (var dateTime in dateTimes)
            {
                foreach (var match in matches)
                {
                    // first we fill all matches for friday and sunday
                    if (dateTime.DayOfWeek == DayOfWeek.Friday || dateTime.DayOfWeek == DayOfWeek.Sunday)
                    {
                        // remove match from list
                        var currentMatch = match;
                        matches.Remove(match);
                        // set dateTime of match and matchday
                        currentMatch.DateTime = dateTime;
                        currentMatch.MatchDay = matchDay;
                        result.Add(currentMatch);
                        // remove from group
                        dateTimes.Remove(dateTime);
                        // only if the date is sunday at 17:30 we have to increase the matchday, otherwise it's not necessary
                        return GenerateMatches(result, matches, dateTimes,
                            dateTime.DayOfWeek == DayOfWeek.Sunday && dateTime.Hour == 17 && dateTime.Minute == 30
                                ? ++matchDay
                                : matchDay);
                    }
                }
            }
            return SetSaturdayMatches(matches, result, dateTimes);
        }

        /// <summary>
        /// Set all left matches to the Saturday.
        /// </summary>
        /// <param name="matches"></param>
        /// <param name="result"></param>
        /// <param name="dateTimes"></param>
        /// <returns></returns>
        private List<Match> SetSaturdayMatches(List<Match> matches, List<Match> result, List<DateTime> dateTimes)
        {
            if (matches.IsEmpty()) return result;
            var matchDay = 1;
            foreach (var dateTime in dateTimes)
            {
                if (matches.Any())
                {
                    // remove match from list
                    var currentMatch = matches.First();
                    matches.Remove(matches.First());
                    // set dateTime of match and matchday
                    currentMatch.DateTime = dateTime;
                    currentMatch.MatchDay = matchDay;
                    result.Add(currentMatch);
                }
                matchDay++;
            }
            return SetSaturdayMatches(matches, result, dateTimes);
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
            if (beginn.Date.Equals(end.Date)) return result;
            switch (beginn.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    result.Add(new DateTime(beginn.Year, beginn.Month, beginn.Day, 20, 30, 0));
                    break;
                case DayOfWeek.Sunday:
                    result.Add(new DateTime(beginn.Year, beginn.Month, beginn.Day, 15, 30, 0));
                    result.Add(new DateTime(beginn.Year, beginn.Month, beginn.Day, 17, 30, 0));
                    break;
                case DayOfWeek.Saturday:
                    result.Add(new DateTime(beginn.Year, beginn.Month, beginn.Day, 15, 30, 0));
                    break;
                case DayOfWeek.Monday:
                    break;
                case DayOfWeek.Tuesday:
                    break;
                case DayOfWeek.Wednesday:
                    break;
                case DayOfWeek.Thursday:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return CreateDateTimes(beginn.AddDays(1), end, result);
        }
    }
}