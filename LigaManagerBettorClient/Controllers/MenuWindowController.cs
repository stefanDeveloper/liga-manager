using System.Collections.ObjectModel;
using System.Linq;
using LigaManagerBettorClient.BettorClientService;
using LigaManagerBettorClient.Frameworks;
using LigaManagerBettorClient.ViewModels;
using LigaManagerBettorClient.Views;
using LigaManagerServer.Models;

namespace LigaManagerBettorClient.Controllers
{
    public class MenuWindowController
    {
        private MainWindow _mainWindow;
        private Bettor _bettor;
        private MenuWindowViewModel _viewModel;
        private BettorClientServiceClient _bettorClient;
        private MenuWindow _view;

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
                SelectedSeason = seasons.ToList().First(),
                BettorRankingCommand = new RelayCommand(ExecuteBettorRankingCommand),
                MatchesCommand = new RelayCommand(ExecuteMatchesCommand),
                TeamsCommand = new RelayCommand(ExecuteTeamsCommand),
            };

            _view.DataContext = _viewModel;
            #endregion

            _mainWindow.Content = _view;
            _mainWindow.Width = 1200;
            _mainWindow.Height = 800;
        }

        #region ExecuteCommands
        public void ExecuteBettorRankingCommand(object obj)
        {
            var bettorRanking = new BettorRankingWindowController();
            bettorRanking.Initialize(_mainWindow, _viewModel.SelectedSeason, _bettor);
        }

        public void ExecuteMatchesCommand(object obj)
        {
            var matches = new MatchesWindowController();
            matches.Initialize(_mainWindow, _viewModel.SelectedSeason, _bettor);
        }

        public void ExecuteTeamsCommand(object obj)
        {
            var teamRankingWindowController = new TeamRankingWindowController();
            teamRankingWindowController.Initialize(_mainWindow, _viewModel.SelectedSeason, _bettor);
        }

        public void ExecuteBackCommand(object obj)
        {
            _view.DataContext = _viewModel;

            _mainWindow.Content = _view;
            _mainWindow.Width = 1200;
            _mainWindow.Height = 800;
        }
        #endregion
    }
}