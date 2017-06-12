using System;
using LigaManagerBettorClient.BettorClientService;
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
        private BettorClientServiceClient _bettorClient;
        public Match Match { get; set; }
        public Bet Bet { get; set; }

        public Bet ShowMatch()
        {
            _view = new DetailMatchWindow();
            _bettorClient = new BettorClientServiceClient();
            _viewModel = new DetailMatchWindowViewModel()
            {
                Bet = Bet,
                Match = Match,
                BetCommand = new RelayCommand(ExecuteBetCommand),
                CancelCommand = new RelayCommand(ExecuteCancelCommand)
            };
            _view.DataContext = _viewModel;

            if (Match.DateTime < DateTime.Now)
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
            if (Bet != null)
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