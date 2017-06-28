using System.Collections.ObjectModel;
using System.Linq;
using LigaManagerBettorClient.BettorClientService;
using LigaManagerBettorClient.Frameworks;
using LigaManagerBettorClient.ViewModels;
using LigaManagerBettorClient.Views;

namespace LigaManagerBettorClient.Controllers
{
    public class BettorRankingWindowController
    {
        private BettorRankingWindow _view;
        private BettorRankingWindowViewModel _viewModel;
        private BettorClientServiceClient _bettorClient;
        private MainWindow _mainWindow;
        private MenuWindowController _menuWindow;
        private Season _selectedSeason;
        private Bettor _bettor;

        public async void Initialize(MainWindow mainWindow, MenuWindowController menuWindow,  Season selectedSeason, Bettor bettor)
        {
            _view = new BettorRankingWindow();
            _bettorClient = new BettorClientServiceClient();
            _mainWindow = mainWindow;
            _menuWindow = menuWindow;
            _selectedSeason = selectedSeason;
            _bettor = bettor;

            #region View and ViewModel
            // Check if service is available
            if (!await BettorClientHelper.IsAvailable(_bettorClient)) return;
            var matches = await _bettorClient.GetMatchesAsync(_selectedSeason);
            // get rankedbettors
            var rankedBettors = await _bettorClient.GetAllRankedBettorsAsync(_selectedSeason);
            // set list for match days
            var matchDays = new ObservableCollection<string> { "Aktuell" };
            if (matches.Any())
            {
                // find max match day
                var max = matches.Max(x => x.MatchDay);
                for (var i = 1; i <= max; i++)
                {
                    matchDays.Add("Spieltag: " + i);
                }
            }
            _viewModel = new BettorRankingWindowViewModel
            {
                Bettors = rankedBettors.ToList(),
                SelectedMatchDay = matchDays.FirstOrDefault(),
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
            _menuWindow.Initialize(_mainWindow,_bettor);
        }

        private async void UpdateMatchDay(object sender, string s)
        {
            var matchday = _view.MatchDayComboBox.SelectedIndex;
            if (matchday == 0)
            {
                var rankedBettors = await _bettorClient.GetAllRankedBettorsAsync(_selectedSeason);
                _viewModel.Bettors = rankedBettors.ToList();
            }
            else
            {
                var rankedBettors = await _bettorClient.GetRankedBettorsAsync(_selectedSeason, matchday);
                _viewModel.Bettors = rankedBettors.ToList();
            }
            
        }
    }
}