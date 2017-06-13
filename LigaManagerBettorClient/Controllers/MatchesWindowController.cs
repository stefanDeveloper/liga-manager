using System.Linq;
using System.Windows;
using System.Windows.Data;
using LigaManagerBettorClient.BettorClientService;
using LigaManagerBettorClient.Frameworks;
using LigaManagerBettorClient.ViewModels;
using LigaManagerBettorClient.Views;
using LigaManagerServer.Models;

namespace LigaManagerBettorClient.Controllers
{
    public class MatchesWindowController
    {
        private MatchesWindow _view;
        private MatchesWindowViewModel _viewModel;
        private BettorClientServiceClient _bettorClient;
        public MainWindow MainWindow { get; set; }
        public Bettor Bettor { get; set; }
        public Season SelectedSeason { get; set; }

        public void Initialize()
        {
            _view = new MatchesWindow();
            _bettorClient = new BettorClientServiceClient();

            var matches = _bettorClient.GetMatches(SelectedSeason);
            var sortedMatches = matches.ToList().OrderBy(a => a.DateTime).ToList();

            var listCollectionView = new ListCollectionView(sortedMatches);
            listCollectionView.GroupDescriptions?.Add(new PropertyGroupDescription("MatchDay"));

            _viewModel = new MatchesWindowViewModel
            {
                SelectedSeason = SelectedSeason,
                Matches = listCollectionView,
                SelectedMatchCommand = new RelayCommand(ExecuteSelectedMatchCommand),
                BackCommand = new RelayCommand(ExecuteBackCommand)
            };

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


        public void ExecuteSelectedMatchCommand(object obj)
        {
            var viewModelSelectedMatch = _viewModel.SelectedMatch;
            var bet = _bettorClient.GetBet(viewModelSelectedMatch, Bettor);
            var detailMatchWindowController = new DetailMatchWindowController
            {
                Match = viewModelSelectedMatch,
                Bet = bet
            };
            var addedObject = detailMatchWindowController.ShowMatch();

            if (addedObject == null) return;
            // At this point we do not know if the bettor still bet for the match, therefore we have to look if it it null or not.
            var isSuccess = bet != null ? _bettorClient.ChangeBet(addedObject) : _bettorClient.AddBet(addedObject);
            
            if (!isSuccess)
            {
                MessageBox.Show(
                    "Der Tipp konnte nicht abgegeben werden! Bitte beachten Sie, dass Sie bis spätestens 30 Minuten vor Spielbeginn tippen können!",
                    "Tipp fehlgeschlagen",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Der Tipp wurde erfolgreich abgegeben, viel Erfolg!", "Tipp erfolgreich",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}