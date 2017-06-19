using System;
using System.Collections.Generic;
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
                if (!(DateTime.Now.AddMinutes(30) < bet.Match.DateTime))
                    return false;
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

        public List<RankedBettor> GetAllRankedBettors(Season season)
        {
            lock (StaticLock)
            {
                var bettors = GetBettors();
                var allBets = GetAllBets();
                var matches = GetMatches(season);
                var result = CalucalteRankedBettors(matches.ToList(), bettors.ToList(), allBets.ToList());

                return SetPlaceOfBettors(result);
            }
        }

        public List<RankedBettor> GetRankedBettors(Season season, int matchday)
        {
            lock (StaticLock)
            {
                var bettors = GetBettors();
                var allBets = GetAllBets();
                var matches = GetMatches(season);

                var filteredMatches = matches.Where(x => x.MatchDay == matchday);
                var filteredBets = allBets.Where(x => matches.Contains(x.Match));
                var result = CalucalteRankedBettors(filteredMatches.ToList(), bettors.ToList(), filteredBets.ToList());
                return SetPlaceOfBettors(result);
            }
        }

        private static List<RankedBettor> SetPlaceOfBettors(List<RankedBettor> bettors)
        {
            bettors.Sort((x, y) => y.Score.CompareTo(x.Score));
            var result = new List<RankedBettor>();
            var i = 1;
            foreach (var rankedBettor in bettors)
            {
                rankedBettor.Place = i;
                result.Add(rankedBettor);
                i++;
            }
            return result;
        }

        public List<RankedTeam> GetAllRankedTeams(Season season)
        {
            lock (StaticLock)
            {
                var matches = GetMatches(season);
                var seasonToTeamRelations = GetTeams(season);
                var result = CalucalteRankedTeams(matches.ToList(), seasonToTeamRelations.ToList());
                return SetPlace(result);
            }
        }

        public List<RankedTeam> GetRankedTeams(Season season, int matchday)
        {
            lock (StaticLock)
            {
                var matches = GetMatches(season);
                var seasonToTeamRelations = GetTeams(season);
                var filteredMatches = matches.ToList().Where(x => x.MatchDay == matchday);
                var result = CalucalteRankedTeams(filteredMatches.ToList(), seasonToTeamRelations.ToList());

                return SetPlace(result);
            }
        }

        private List<RankedTeam> SetPlace(List<RankedTeam> rankedTeams)
        {
            var result = new List<RankedTeam>();
            rankedTeams.Sort((x, y) => y.Score.CompareTo(x.Score));
            var i = 1;
            foreach (var rankedTeam in rankedTeams)
            {
                rankedTeam.Place = i;
                result.Add(rankedTeam);
                i++;
            }
            return result;
        }

        private List<RankedBettor> CalucalteRankedBettors(List<Match> matches,
            List<Bettor> bettors, List<Bet> bets)
        {
            var result = new List<RankedBettor>();
            foreach (var bettor in bettors)
            {
                var rankedBettor = new RankedBettor {Bettor = bettor};
                foreach (var match in matches)
                {
                    var find = bets.Find(x => x.Match.Equals(match) && x.Bettor.Equals(bettor));
                    if (find != null)
                    {
                        var isHomeWin = match.HomeTeamScore > match.AwayTeamScore;
                        var isAwayWin = match.AwayTeamScore > match.HomeTeamScore;
                        if (find.HomeTeamScore == match.HomeTeamScore && find.AwayTeamScore == match.AwayTeamScore)
                        {
                            rankedBettor.Score += 4;
                        }
                        else if (isAwayWin == (find.AwayTeamScore > find.HomeTeamScore) &&
                                 (find.AwayTeamScore - find.HomeTeamScore) ==
                                 (match.AwayTeamScore - match.HomeTeamScore))
                        {
                            rankedBettor.Score += 3;
                        }
                        else if (isHomeWin == (find.HomeTeamScore > find.AwayTeamScore) &&
                                 (find.HomeTeamScore - find.AwayTeamScore) ==
                                 (match.HomeTeamScore - match.AwayTeamScore))
                        {
                            rankedBettor.Score += 3;
                        }
                        else if (((find.HomeTeamScore > find.AwayTeamScore) && isHomeWin) ||
                                 ((find.AwayTeamScore < find.HomeTeamScore) && isAwayWin))
                        {
                            rankedBettor.Score += 2;
                        }
                    }
                }
                result.Add(rankedBettor);
            }
            return result;
        }

        private List<RankedTeam> CalucalteRankedTeams(List<Match> matches, List<SeasonToTeamRelation> seasonToTeamRelations)
        {
            var result = new List<RankedTeam>();
            foreach (var team in seasonToTeamRelations)
            {
                var rankedTeam = new RankedTeam { Team = team.Team };
                foreach (var match in matches)
                {
                    if (match.HomeTeam.Equals(team.Team))
                    {
                        rankedTeam.NumberOfMatches += 1;
                        if (match.HomeTeamScore > match.AwayTeamScore)
                        {
                            rankedTeam.NumberOfWins += 1;
                            rankedTeam.Score += 3;
                            var goalDifference = match.HomeTeamScore - match.AwayTeamScore;
                            rankedTeam.GoalDifference += goalDifference;
                        }
                        else if (match.HomeTeamScore == match.AwayTeamScore)
                        {
                            rankedTeam.NumberOfTieds += 1;
                            rankedTeam.Score += 1;
                            // no goal difference need, because it's zero.
                        }
                        else
                        {
                            rankedTeam.NumberOfLooses += 1;
                            var goalDifference = match.HomeTeamScore - match.AwayTeamScore;
                            rankedTeam.GoalDifference += goalDifference;
                        }
                    }
                    else if (match.AwayTeam.Equals(team.Team))
                    {
                        rankedTeam.NumberOfMatches += 1;
                        if (match.AwayTeamScore > match.HomeTeamScore)
                        {
                            rankedTeam.NumberOfWins += 1;
                            rankedTeam.Score += 3;
                            var goalDifference = match.AwayTeamScore - match.HomeTeamScore;
                            rankedTeam.GoalDifference += goalDifference;
                        }
                        else if (match.HomeTeamScore == match.AwayTeamScore)
                        {
                            rankedTeam.NumberOfTieds += 1;
                            rankedTeam.Score += 1;
                        }
                        else
                        {
                            rankedTeam.NumberOfLooses += 1;
                            var goalDifference = match.AwayTeamScore - match.HomeTeamScore;
                            rankedTeam.GoalDifference += goalDifference;
                        }
                    }
                }
                result.Add(rankedTeam);
            }
            return result;
        }
    }
}