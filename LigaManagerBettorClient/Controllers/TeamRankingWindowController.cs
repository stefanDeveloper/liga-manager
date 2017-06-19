using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using LigaManagerBettorClient.BettorClientService;
using LigaManagerBettorClient.Frameworks;
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

        private MainWindow _mainWindow;
        private Season _selectedSeason;
        private Bettor _bettor;


        public void Initialize(MainWindow mainWindow, Season selectedSeason, Bettor bettor)
        {
            _view = new TeamRankingWindow();
            _bettorClient = new BettorClientServiceClient();
            _mainWindow = mainWindow;
            _selectedSeason = selectedSeason;
            _bettor = bettor;

            #region View and ViewModel
            var matches = _bettorClient.GetMatches(_selectedSeason);
            var max = matches.ToList().Max(x => x.MatchDay);

            var rankedTeams = GetRankedTeams(0);
            var matchDays = new ObservableCollection<string> { "Aktuell" };
            for (var i = 1; i <= max; i++)
            {
                matchDays.Add("Spieltag: " + i);
            }
            _viewModel = new TeamRankingWindowViewModel
            {
                SelectedMatchDay = matchDays.First(),
                MatchDays = matchDays,
                Teams = rankedTeams,
                BackCommand = new RelayCommand(ExecuteBackCommand)
            };
            _viewModel.SelectionMatchDayChanged += UpdateMatchDay;
            _view.DataContext = _viewModel;
            #endregion

            _mainWindow.Width = 1200;
            _mainWindow.Height = 800;
            _mainWindow.Content = _view;
        }

        private void ExecuteBackCommand(object obj)
        {
            var menuWindow = new MenuWindowController();
            menuWindow.Initialize(_mainWindow, _bettor);
        }

        private void UpdateMatchDay(object sender, string s)
        {
            var matchday = 0;
            if (!s.Equals("Aktuell"))
            {
                var resultString = Regex.Match(s, @"\d+").Value;
                matchday = int.Parse(resultString);
            }
            var rankedTeams = GetRankedTeams(matchday);
            _viewModel.Teams = rankedTeams;
        }

        private List<RankedTeam> GetRankedTeams(int matchday)
        {
            List<RankedTeam> result;
            if (matchday == 0)
            {
                var matches = _bettorClient.GetMatches(_selectedSeason);
                var seasonToTeamRelations = _bettorClient.GetTeams(_selectedSeason);
                result = CalucalteRankedTeams(matches.ToList(), seasonToTeamRelations.ToList());
            }
            else
            {
                var matches = _bettorClient.GetMatches(_selectedSeason);
                var seasonToTeamRelations = _bettorClient.GetTeams(_selectedSeason);
                var filteredMatches = matches.ToList().Where(x => x.MatchDay == matchday);
                result = CalucalteRankedTeams(filteredMatches.ToList(), seasonToTeamRelations.ToList());
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