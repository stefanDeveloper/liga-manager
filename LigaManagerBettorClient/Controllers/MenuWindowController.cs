﻿using System.Collections.ObjectModel;
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
                Seasons = new ObservableCollection<Season>(seasons.ToList()),
                SelectedSeason = seasons.ToList().First(),
                BettorRankingCommand = new RelayCommand(ExecuteBettorRankingCommand),
                MatchesCommand = new RelayCommand(ExecuteMatchesCommand),
                TeamsCommand = new RelayCommand(ExecuteTeamsCommand),
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
                SelectedSeason = _viewModel.SelectedSeason,
                Bettor = Bettor

            };
            bettorRanking.Initialize();
        }

        public void ExecuteMatchesCommand(object obj)
        {
            var matches = new MatchesWindowController
            {
                MainWindow = MainWindow,
                SelectedSeason = _viewModel.SelectedSeason,
                Bettor = Bettor

            };
            matches.Initialize();
        }

        public void ExecuteTeamsCommand(object obj)
        {
            var teamRankingWindowController = new TeamRankingWindowController
            {
                MainWindow = MainWindow,
                SelectedSeason = _viewModel.SelectedSeason,
                Bettor = Bettor
            };
            teamRankingWindowController.Initialize();
        }

        public void ExecuteBackCommand(object obj)
        {
            _view.DataContext = _viewModel;

            MainWindow.Content = _view;
            MainWindow.Width = 1200;
            MainWindow.Height = 800;
        }
    }
}