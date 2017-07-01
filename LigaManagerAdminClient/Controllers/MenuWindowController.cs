using LigaManagerAdminClient.ViewModels;
using LigaManagerAdminClient.Views;
using LigaManagerBettorClient.Frameworks;

namespace LigaManagerAdminClient.Controllers
{
    public class MenuWindowController
    {
        private MainWindow _mainWindow;
        private MenuWindowViewModel _viewModel;
        private MenuWindow _view;

        public void Initialize(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;

            #region View and ViewModel
            _view = new MenuWindow();
            _viewModel = new MenuWindowViewModel
            {
                BettorCommand = new RelayCommand(ExecuteBettorCommand),
                TeamCommand = new RelayCommand(ExecuteTeamCommand),
                SeasonCommand = new RelayCommand(ExecuteSeasonCommand),
                MatchesCommand = new RelayCommand(ExecuteMatchesCommand)
            };

            _view.DataContext = _viewModel;
            #endregion

            _mainWindow.Content = _view;
            _mainWindow.Show();
        }

        #region Commands
        private void ExecuteBettorCommand(object obj)
        {
            var bettorWindow = new BettorListWindowController();
            bettorWindow.Initialize(_mainWindow);
        }

        private void ExecuteTeamCommand(object obj)
        {
            var teamWindow = new TeamListWindowController();
            teamWindow.Initialize(_mainWindow);
        }

        private void ExecuteSeasonCommand(object obj)
        {
            var seasonWindow = new SeasonListWindowController();
            seasonWindow.Initialize(_mainWindow);
        }

        private void ExecuteMatchesCommand(object obj)
        {
            var seasonWindow = new MatchesWindowController();
            seasonWindow.Initialize(_mainWindow);
        }
        #endregion
    }
}
