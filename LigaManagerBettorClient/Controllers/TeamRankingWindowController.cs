using LigaManagerBettorClient.BettorClientService;
using LigaManagerBettorClient.ViewModels;
using LigaManagerBettorClient.Views;

namespace LigaManagerBettorClient.Controllers
{
    public class TeamRankingWindowController
    {
        private TeamRankingWindow _view;
        private TeamRankingWindowViewModel _viewModel;
        private BettorClientServiceClient _bettorClient;

        public MainWindow MainWindow { get; set; }
        public void Initialize()
        {
            _view = new TeamRankingWindow();
            _bettorClient = new BettorClientServiceClient();
            _viewModel = new TeamRankingWindowViewModel
            {
            };
            _view.DataContext = _viewModel;

            MainWindow.Width = 1200;
            MainWindow.Height = 800;
            MainWindow.Content = _view;
        }
    }
}