using System;
using LigaManagerBettorClient.Frameworks;
using LigaManagerBettorClient.ViewModels;
using LigaManagerBettorClient.Views;
using LigaManagerServer.Models;

namespace LigaManagerBettorClient.Controllers
{
    public class DetailMatchWindowController
    {
        private DetailMatchWindow _view;
        private DetailMatchWindowViewModel _viewModel;
        public Match Match { get; set; }
        public Bet Bet { get; set; }

        public Bet ShowMatch()
        {
            _view = new DetailMatchWindow();

            #region View and ViewModel
            _viewModel = new DetailMatchWindowViewModel
            {
                Bet = Bet,
                Match = Match,
                BetCommand = new RelayCommand(ExecuteBetCommand),
                CancelCommand = new RelayCommand(ExecuteCancelCommand)
            };
            _view.DataContext = _viewModel;
            #endregion

            // check if the bet is valid
            if (Match.DateTime < DateTime.Now.AddMinutes(30))
            {
                _view.AwayTeamBet.IsEnabled = false;
                _view.HomeTeamBet.IsEnabled = false;
                _view.BetButton.IsEnabled = false;
            }
            return _view.ShowDialog() == true ? _viewModel.Bet : null;
        }
        private void ExecuteBetCommand(object obj)
        {
            _view.DialogResult = true;
            if (Bet != null && Match.DateTime < DateTime.Now.AddMinutes(30))
            {
                Bet.DateTime = DateTime.Now;
                _view.Close();
            }
            else
            {
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