using System;
using System.Windows;
using LigaManagerBettorClient.BettorClientService;
using LigaManagerBettorClient.Frameworks;
using LigaManagerBettorClient.Models;
using LigaManagerBettorClient.ViewModels;
using LigaManagerBettorClient.Views;

namespace LigaManagerBettorClient.Controllers
{
    public class DetailMatchWindowController
    {
        private DetailMatchWindow _view;
        private DetailMatchWindowViewModel _viewModel;
        private State _state = State.Added;
        public Match Match { get; set; }
        public Bet Bet { get; set; }

        public State ShowMatch()
        {
            _view = new DetailMatchWindow();

            #region View and ViewModel
            _viewModel = new DetailMatchWindowViewModel
            {
                Bet = Bet,
                Match = Match,
                BetCommand = new RelayCommand(ExecuteBetCommand),
                CancelCommand = new RelayCommand(ExecuteCancelCommand),
                DeleteBetCommand = new RelayCommand(ExecuteDeleteCommand)
            };
            _view.DataContext = _viewModel;
            #endregion

            // check if the match is valid, if not there is no valid score for both sides.
            if (Match.DateTime < DateTime.Now.AddMinutes(30))
            {
                _view.AwayTeamBet.IsEnabled = false;
                _view.HomeTeamBet.IsEnabled = false;
                _view.BetButton.IsEnabled = false;
            }
            else
            {
                // na abbreviation for not available
                _view.AwayTeamScoreTextBlock.Text = "N.A.";
                _view.HomeTeamScoreTextBlock.Text = "N.A.";
            }
            // disable delete btn if bet does not exists
            if (Bet == null || Match.DateTime < DateTime.Now.AddMinutes(30)) _view.DeleteBetButton.IsEnabled = false;
            // change title if bet exists
            if (Bet != null) _view.BetButton.Content = "Tipp speichern";
            return _view.ShowDialog() == true ? _state : State.Abort;
        }

        private void ExecuteDeleteCommand(object obj)
        {
            // Check if the user really want to delete the bettor
            var messageBoxResult = MessageBox.Show("Sind Sie sicher, dass der Tipp gelöscht werden soll!",
                "Tipp löschen",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult != MessageBoxResult.Yes) return;
            _state = State.Deleted;
            _view.DialogResult = true;
            _view.Close();
        }

        private void ExecuteBetCommand(object obj)
        {
            _view.DialogResult = true;
            if (Bet != null && Match.DateTime < DateTime.Now.AddMinutes(30))
            {
                // set new DateTime of bet
                Bet.DateTime = DateTime.Now;
                _state = State.Changed;
                _view.Close();
            }
            else if (Bet == null && Match.DateTime > DateTime.Now.AddMinutes(30))
            {
                //add new bet
                _viewModel.Bet = new Bet
                {
                    Match = _viewModel.Match,
                    DateTime = DateTime.Now,
                    AwayTeamScore = int.Parse(_view.AwayTeamBet.Text),
                    HomeTeamScore = int.Parse(_view.HomeTeamBet.Text),
                };
                _state = State.Added;
                _view.Close();
            }
        }

        private void ExecuteCancelCommand(object obj)
        {
            _view.DialogResult = false;
            _view.Close();
        }
    }
}