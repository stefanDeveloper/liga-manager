using System.Collections.ObjectModel;
using System.Linq;
using LigaManagerBettorClient.BettorClientService;
using LigaManagerBettorClient.Frameworks;
using LigaManagerBettorClient.ViewModels;
using LigaManagerBettorClient.Views;

namespace LigaManagerBettorClient.Controllers
{
    public class MenuWindowController
    {
        private MainWindow _mainWindow;
        private Bettor _bettor;
        private MenuWindowViewModel _viewModel;
        private BettorClientServiceClient _bettorClient;
        private MenuWindow _view;

        private Season _selectedSeason;

        public void Initialize(MainWindow mainWindow, Bettor bettor)
        {
            _view = new MenuWindow();
            _bettorClient = new BettorClientServiceClient();
            _mainWindow = mainWindow;
            _bettor = bettor;
            // Get all seasons
            var seasons = _bettorClient.GetSeasons();

            #region View and ViewModel
            _viewModel = new MenuWindowViewModel
            {
                Bettor = _bettor,
                Seasons = new ObservableCollection<Season>(seasons.ToList()),
                SelectedSeason = _selectedSeason ?? seasons.FirstOrDefault(),
                BettorRankingCommand = new RelayCommand(ExecuteBettorRankingCommand),
                MatchesCommand = new RelayCommand(ExecuteMatchesCommand),
                TeamsCommand = new RelayCommand(ExecuteTeamsCommand),
            };
            _selectedSeason = _selectedSeason != null ? _viewModel.SelectedSeason : seasons.FirstOrDefault();
            _view.DataContext = _viewModel;
            #endregion

            _mainWindow.Content = _view;
            _mainWindow.Width = 800;
            _mainWindow.Height = 600;
        }

        #region ExecuteCommands
        public async void ExecuteBettorRankingCommand(object obj)
        {
            // Check if service is available
            if (!await BettorClientHelper.IsAvailable(_bettorClient)) return;
            var bettorRanking = new BettorRankingWindowController();
            _selectedSeason = _viewModel.SelectedSeason;
            bettorRanking.Initialize(_mainWindow, this, _viewModel.SelectedSeason, _bettor);
        }

        public async void ExecuteMatchesCommand(object obj)
        {
            // Check if service is available
            if (!await BettorClientHelper.IsAvailable(_bettorClient)) return;
            var matches = new MatchesWindowController();
            _selectedSeason = _viewModel.SelectedSeason;
            matches.Initialize(_mainWindow, this, _viewModel.SelectedSeason, _bettor);
        }

        public async void ExecuteTeamsCommand(object obj)
        {
            // Check if service is available
            if (!await BettorClientHelper.IsAvailable(_bettorClient)) return;
            var teamRankingWindowController = new TeamRankingWindowController();
            _selectedSeason = _viewModel.SelectedSeason;
            teamRankingWindowController.Initialize(_mainWindow, this, _viewModel.SelectedSeason, _bettor);
        }

        #endregion
    }
}