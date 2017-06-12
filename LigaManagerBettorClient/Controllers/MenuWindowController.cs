using LigaManagerBettorClient.Frameworks;
using LigaManagerBettorClient.ViewModels;
using LigaManagerBettorClient.Views;

namespace LigaManagerBettorClient.Controllers
{
    public class MenuWindowController
    {
        public MainWindow MainWindow { get; set; }
        private MenuWindowViewModel _viewModel;
        private MenuWindow _view;
        public void Initialize()
        {
            _view = new MenuWindow();
            _viewModel = new MenuWindowViewModel
            {
                BettorRankingCommand = new RelayCommand(ExecuteBettorRankingCommand),
                MatchesCommand = new RelayCommand(ExecuteMatchesCommand),
                TeamsCommand = new RelayCommand(ExecuteTeamsCommand)
            };

            _view.DataContext = _viewModel;

            MainWindow.Content = _view;
            MainWindow.Width = 1200;
            MainWindow.Height = 800;
        }

        public void ExecuteBettorRankingCommand(object obj)
        {
            
        }

        public void ExecuteMatchesCommand(object obj)
        {

        }

        public void ExecuteTeamsCommand(object obj)
        {

        }
    }
}