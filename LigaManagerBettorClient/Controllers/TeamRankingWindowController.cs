using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using LigaManagerBettorClient.BettorClientService;
using LigaManagerBettorClient.Models;
using LigaManagerBettorClient.ViewModels;
using LigaManagerBettorClient.Views;
using LigaManagerServer.Models;
using Match = LigaManagerServer.Models.Match;

namespace LigaManagerBettorClient.Controllers
{
    public class TeamRankingWindowController
    {
        private TeamRankingWindow _view;
        private TeamRankingWindowViewModel _viewModel;
        private BettorClientServiceClient _bettorClient;

        public MainWindow MainWindow { get; set; }
        public Season SelecetedSeason { get; set; }
        public Bettor Bettor { get; set; }

        public void Initialize()
        {
            _view = new TeamRankingWindow();
            _bettorClient = new BettorClientServiceClient();
            var matches = _bettorClient.GetMatches(SelecetedSeason);
            var max = matches.ToList().Max(x => x.MatchDay);

            var rankedTeams = GetRankedTeam(0);
            var matchDays = new ObservableCollection<string> {"Aktuell"};
            for (var i = 1; i <= max; i++)
            {
                matchDays.Add("Spieltag: " + i);
            }
            _viewModel = new TeamRankingWindowViewModel
            {
                SelectedMatchDay = matchDays.First(),
                MatchDays = matchDays,
                Teams = rankedTeams
            };
            _viewModel.InnerButtonClick += UpdateMatchDay;
            _view.DataContext = _viewModel;
            

            MainWindow.Width = 1200;
            MainWindow.Height = 800;
            MainWindow.Content = _view;
        }

        private void UpdateMatchDay(object sender, string s)
        {
            var matchday = 0;
            if (!s.Equals("Aktuell"))
            {
                var resultString = Regex.Match(s, @"\d+").Value;
                matchday = int.Parse(resultString);
            }
            var rankedTeams = GetRankedTeam(matchday);
            _viewModel.Teams = rankedTeams;
        }

        private List<RankedTeam> GetRankedTeam(int matchday)
        {
            var result = new List<RankedTeam>();
            if (matchday == 0)
            {
                var matches = _bettorClient.GetMatches(SelecetedSeason);
                var seasonToTeamRelations = _bettorClient.GetTeams(SelecetedSeason);
                result = CalucalteRankedTeam(matches.ToList(), seasonToTeamRelations.ToList());
            }
            else
            {
                var matches = _bettorClient.GetMatches(SelecetedSeason);
                var seasonToTeamRelations = _bettorClient.GetTeams(SelecetedSeason);
                var filteredMatches = matches.ToList().Where(x => x.MatchDay == matchday);
                result = CalucalteRankedTeam(filteredMatches.ToList(), seasonToTeamRelations.ToList());
            }
            result.Sort((x,y) => y.Score.CompareTo(x.Score));   
            var resultWithPlace = new List<RankedTeam>();
            var i = 1;
            foreach (var rankedTeam in result)
            {
                rankedTeam.Place = i;
                resultWithPlace.Add(rankedTeam);
                i++;
            }
            return resultWithPlace;
        }

        private List<RankedTeam> CalucalteRankedTeam(List<Match> matches, List<SeasonToTeamRelation> seasonToTeamRelations)
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