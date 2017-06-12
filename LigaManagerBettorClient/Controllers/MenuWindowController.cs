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
        public MainWindow MainWindow { get; set; }
        public Bettor Bettor { get; set; }
        private MenuWindowViewModel _viewModel;
        private BettorClientServiceClient _bettorClient;
        private MenuWindow _view;
        public void Initialize()
        {
            _view = new MenuWindow();
            _bettorClient = new BettorClientServiceClient();
            var seasons = _bettorClient.GetSeasons();
            _viewModel = new MenuWindowViewModel
            {
                Bettor = Bettor,
                Seasons = seasons.ToList(),
                SelecetedSeason = seasons.ToList().First(),
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
            var bettorRanking = new BettorRankingWindowController
            {
                MainWindow = MainWindow,
                
            };
            bettorRanking.Initialize();
        }

        public void ExecuteMatchesCommand(object obj)
        {
            var matches = new MatchesWindowController
            {
                MainWindow = MainWindow,
                SelectedSeason = _viewModel.SelecetedSeason,
                Bettor = Bettor,
                
            };
            matches.Initialize();
        }

        public void ExecuteTeamsCommand(object obj)
        {
        }
    }
}