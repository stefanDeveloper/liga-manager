using System.Linq;
using System.Windows;
using System.Windows.Data;
using LigaManagerBettorClient.BettorClientService;
using LigaManagerBettorClient.Frameworks;
using LigaManagerBettorClient.Models;
using LigaManagerBettorClient.ViewModels;
using LigaManagerBettorClient.Views;

namespace LigaManagerBettorClient.Controllers
{
    public class MatchesWindowController
    {
        private MatchesWindow _view;
        private MatchesWindowViewModel _viewModel;
        private BettorClientServiceClient _bettorClient;
        private MainWindow _mainWindow;
        private Season _selectedSeason;
        private Bettor _bettor;

        public void Initialize(MainWindow mainWindow, Season selectedSeason, Bettor bettor)
        {
            _view = new MatchesWindow();
            _bettorClient = new BettorClientServiceClient();
            _mainWindow = mainWindow;
            _selectedSeason = selectedSeason;
            _bettor = bettor;

            #region View and ViewModel
            // Get all Matches and order them by DateTime
            var matches = _bettorClient.GetMatches(_selectedSeason);
            var sortedMatches = matches.ToList().OrderBy(a => a.DateTime).ToList();

            // Group List by MatchDay's
            var listCollectionView = new ListCollectionView(sortedMatches);
            listCollectionView.GroupDescriptions?.Add(new PropertyGroupDescription("MatchDay"));

            _viewModel = new MatchesWindowViewModel
            {
                SelectedSeason = _selectedSeason,
                Matches = listCollectionView,
                SelectedMatch = sortedMatches.FirstOrDefault(),
                SelectedMatchCommand = new RelayCommand(ExecuteSelectedMatchCommand),
                BackCommand = new RelayCommand(ExecuteBackCommand)
            };

            _view.DataContext = _viewModel;
            #endregion

            _mainWindow.Content = _view;
        }

        private void ExecuteBackCommand(object obj)
        {
            var menuWindow = new MenuWindowController();
            menuWindow.Initialize(_mainWindow, _bettor);
        }


        public async void ExecuteSelectedMatchCommand(object obj)
        {
            var viewModelSelectedMatch = _viewModel.SelectedMatch;
            var bet = _bettorClient.GetBet(viewModelSelectedMatch, _bettor);
            var detailMatchWindowController = new DetailMatchWindowController
            {
                Match = viewModelSelectedMatch,
                Bet = bet
            };
            var betState = detailMatchWindowController.ShowMatch();

            if (betState == State.Abort) return;
            // At this point we do not know if the bettor still bet for the match, therefore we have to look if it it null or not.
            bool isSuccess = false;
            switch (betState)
            {
                case State.Changed:
                    isSuccess = await _bettorClient.ChangeBetAsync(detailMatchWindowController.Bet);
                    break;
                case State.Added:
                    detailMatchWindowController.Bet.Bettor = _bettor;
                    isSuccess = await _bettorClient.AddBetAsync(detailMatchWindowController.Bet);
                    break;
                case State.Deleted:
                    isSuccess = await _bettorClient.RemoveBetAsync(detailMatchWindowController.Bet);
                    break;
            }

            if (!isSuccess)
            {
                MessageBox.Show(
                    "Der Tipp konnte nicht abgegeben werden! Bitte beachten Sie, dass Sie bis spätestens 30 Minuten vor Spielbeginn tippen können!",
                    "Tipp fehlgeschlagen",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            switch (betState)
            {
                case State.Added:
                case State.Changed:
                    MessageBox.Show("Der Tipp wurde erfolgreich abgegeben, viel Erfolg!", "Tipp erfolgreich",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case State.Deleted:
                    MessageBox.Show("Der Tipp wurde erfolgreich gelöscht!", "Tipp erfolgreich",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }
        }
    }
}