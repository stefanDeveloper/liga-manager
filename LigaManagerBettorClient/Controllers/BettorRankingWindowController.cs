using System.Linq;
using LigaManagerBettorClient.BettorClientService;
using LigaManagerBettorClient.ViewModels;
using LigaManagerBettorClient.Views;

namespace LigaManagerBettorClient.Controllers
{
    public class BettorRankingWindowController
    {
        public MainWindow MainWindow { get; set; }
        private BettorRankingWindow _view;
        private BettorRankingWindowViewModel _viewModel;
        private BettorClientServiceClient _bettorClient;
        public void Initialize()
        {
            _view = new BettorRankingWindow();
            _bettorClient = new BettorClientServiceClient();
            var bettors = _bettorClient.GetBettors();
            _viewModel = new BettorRankingWindowViewModel
            {
                Bettors = bettors.ToList()
            };

            _view.DataContext = _viewModel;
            MainWindow.Width = 1200;
            MainWindow.Height = 800;
            MainWindow.Content = _view;
        }
    }
}