using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using LigaManagerBettorClient.Annotations;
using LigaManagerBettorClient.BettorClientService;
using LigaManagerBettorClient.Frameworks;
using LigaManagerBettorClient.Models;
using LigaManagerBettorClient.ViewModels;
using LigaManagerBettorClient.Views;
using LigaManagerServer.Models;
using Match = LigaManagerServer.Models.Match;

namespace LigaManagerBettorClient.Controllers
{
    public class BettorRankingWindowController
    {
        public MainWindow MainWindow { get; set; }
        public Season SelectedSeason { get; set; }
        public Bettor Bettor { get; set; }

        private BettorRankingWindow _view;
        private BettorRankingWindowViewModel _viewModel;
        private BettorClientServiceClient _bettorClient;
        public void Initialize()
        {
            _view = new BettorRankingWindow();
            _bettorClient = new BettorClientServiceClient();
            var matches = _bettorClient.GetMatches(SelectedSeason);
            var max = matches.ToList().Max(x => x.MatchDay);
            var rankedBettors = GetRankedBettors(0);
            var matchDays = new ObservableCollection<string> { "Aktuell" };
            for (var i = 1; i <= max; i++)
            {
                matchDays.Add("Spieltag: " + i);
            }
            _viewModel = new BettorRankingWindowViewModel
            {
                Bettors = rankedBettors,
                SelectedMatchDay = matchDays.First(),
                MatchDays = matchDays,
                BackCommand = new RelayCommand(ExecuteBackCommand)
            };
            _viewModel.SelectionMatchDayChanged += UpdateMatchDay;
            // set view of Window
            _view.DataContext = _viewModel;
            MainWindow.Width = 1200;
            MainWindow.Height = 800;
            MainWindow.Content = _view;
        }

        private void ExecuteBackCommand(object obj)
        {
            var menuWindow = new MenuWindowController
            {
                Bettor = Bettor,
                MainWindow = MainWindow
            };
            menuWindow.Initialize();
        }
        private void UpdateMatchDay(object sender, string s)
        {
            var matchday = 0;
            if (!s.Equals("Aktuell"))
            {
                var resultString = Regex.Match(s, @"\d+").Value;
                matchday = int.Parse(resultString);
            }
            var rankedBettors = GetRankedBettors(matchday);
            _viewModel.Bettors = rankedBettors;
        }

        private List<RankedBettor> GetRankedBettors(int matchday)
        {
            List<RankedBettor> result = null;
            if (matchday == 0)
            {
                var bettors = _bettorClient.GetBettors();
                var allBets = _bettorClient.GetAllBets();
                var matches = _bettorClient.GetMatches(SelectedSeason);
                result = CalucalteRankedBettors(matches.ToList(), bettors.ToList(), allBets.ToList());
            }
            else
            {
                var bettors = _bettorClient.GetBettors();
                var allBets = _bettorClient.GetAllBets();
                var matches = _bettorClient.GetMatches(SelectedSeason);

                var filteredMatches = matches.Where(x => x.MatchDay == matchday);
                var filteredBets = allBets.Where(x => matches.Contains(x.Match));
                result = CalucalteRankedBettors(filteredMatches.ToList(), bettors.ToList(), filteredBets.ToList());
            }
            result.Sort((x, y) => y.Score.CompareTo(x.Score));
            var resultWithPlace = new List<RankedBettor>();
            var i = 1;
            foreach (var rankedBettor in result)
            {
                rankedBettor.Place = i;
                resultWithPlace.Add(rankedBettor);
                i++;
            }
            return resultWithPlace;
        }

        private List<RankedBettor> CalucalteRankedBettors([NotNull] List<Match> matches,
            [NotNull] List<Bettor> bettors, [NotNull] List<Bet> bets)
        {
            if (matches == null) throw new ArgumentNullException(nameof(matches));
            if (bettors == null) throw new ArgumentNullException(nameof(bettors));
            if (bets == null) throw new ArgumentNullException(nameof(bets));
            var result = new List<RankedBettor>();
            foreach (var bettor in bettors)
            {
                var rankedBettor = new RankedBettor { Bettor = bettor };
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
                        } else if (isAwayWin == (find.AwayTeamScore > find.HomeTeamScore) &&
                                   (find.AwayTeamScore - find.HomeTeamScore) ==
                                   (match.AwayTeamScore - match.HomeTeamScore))
                        {
                            rankedBettor.Score += 3;
                        } else if (isHomeWin == (find.HomeTeamScore > find.AwayTeamScore) &&
                                   (find.HomeTeamScore - find.AwayTeamScore) ==
                                   (match.HomeTeamScore - match.AwayTeamScore))
                        {
                            rankedBettor.Score += 3;
                        } else if (((find.HomeTeamScore > find.AwayTeamScore) && isHomeWin) || ((find.AwayTeamScore < find.HomeTeamScore) && isAwayWin))
                        {
                            rankedBettor.Score += 2;
                        }
                    }
                }
                result.Add(rankedBettor);
            }
            return result;
        }
    }
}