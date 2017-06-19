using System.Collections.ObjectModel;
using System.Linq;
using LigaManagerBettorClient.BettorClientService;
using LigaManagerBettorClient.Frameworks;
using LigaManagerBettorClient.ViewModels;
using LigaManagerBettorClient.Views;
using LigaManagerServer.Models;

namespace LigaManagerBettorClient.Controllers
{
    public class BettorRankingWindowController
    {
        private BettorRankingWindow _view;
        private BettorRankingWindowViewModel _viewModel;
        private BettorClientServiceClient _bettorClient;
        private MainWindow _mainWindow;
        private Season _selectedSeason;
        private Bettor _bettor;

        public void Initialize(MainWindow mainWindow, Season selectedSeason, Bettor bettor)
        {
            _view = new BettorRankingWindow();
            _bettorClient = new BettorClientServiceClient();
            _mainWindow = mainWindow;
            _selectedSeason = selectedSeason;
            _bettor = bettor;

            #region View and ViewModel
            var matches = _bettorClient.GetMatches(_selectedSeason);
            // find max match day
            var max = matches.ToList().Max(x => x.MatchDay);
            // get rankedbettors
            var rankedBettors = _bettorClient.GetAllRankedBettors(_selectedSeason);
            // set list for match days
            var matchDays = new ObservableCollection<string> { "Aktuell" };
            for (var i = 1; i <= max; i++)
            {
                matchDays.Add("Spieltag: " + i);
            }
            _viewModel = new BettorRankingWindowViewModel
            {
                Bettors = rankedBettors.ToList(),
                SelectedMatchDay = matchDays.First(),
                MatchDays = matchDays,
                BackCommand = new RelayCommand(ExecuteBackCommand)
            };
            _viewModel.SelectionMatchDayChanged += UpdateMatchDay;
            // set view of Window
            _view.DataContext = _viewModel;
            #endregion

            _mainWindow.Content = _view;
        }

        private void ExecuteBackCommand(object obj)
        {
            var menuWindow = new MenuWindowController();
            menuWindow.Initialize(_mainWindow,_bettor);
        }

        private void UpdateMatchDay(object sender, string s)
        {
            var matchday = _view.MatchDayComboBox.SelectedIndex;
            if (matchday == 0)
            {
                var rankedBettors = _bettorClient.GetAllRankedBettors(_selectedSeason);
                _viewModel.Bettors = rankedBettors.ToList();
            }
            else
            {
                var rankedBettors = _bettorClient.GetRankedBettors(_selectedSeason, matchday);
                _viewModel.Bettors = rankedBettors.ToList();
            }
            
        }
    }
}