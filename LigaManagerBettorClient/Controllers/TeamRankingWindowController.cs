﻿using System.Collections.ObjectModel;
using System.Linq;
using LigaManagerBettorClient.BettorClientService;
using LigaManagerBettorClient.Frameworks;
using LigaManagerBettorClient.ViewModels;
using LigaManagerBettorClient.Views;
using LigaManagerServer.Models;

namespace LigaManagerBettorClient.Controllers
{
    public class TeamRankingWindowController
    {
        private TeamRankingWindow _view;
        private TeamRankingWindowViewModel _viewModel;
        private BettorClientServiceClient _bettorClient;

        private MainWindow _mainWindow;
        private Season _selectedSeason;
        private Bettor _bettor;


        public void Initialize(MainWindow mainWindow, Season selectedSeason, Bettor bettor)
        {
            _view = new TeamRankingWindow();
            _bettorClient = new BettorClientServiceClient();
            _mainWindow = mainWindow;
            _selectedSeason = selectedSeason;
            _bettor = bettor;

            #region View and ViewModel
            var matches = _bettorClient.GetMatches(_selectedSeason);
            var max = matches.ToList().Max(x => x.MatchDay);

            var rankedTeams = _bettorClient.GetAllRankedTeams(_selectedSeason);
            var matchDays = new ObservableCollection<string> { "Aktuell" };
            for (var i = 1; i <= max; i++)
            {
                matchDays.Add("Spieltag: " + i);
            }
            _viewModel = new TeamRankingWindowViewModel
            {
                SelectedMatchDay = matchDays.First(),
                MatchDays = matchDays,
                Teams = rankedTeams.ToList(),
                BackCommand = new RelayCommand(ExecuteBackCommand)
            };
            _viewModel.SelectionMatchDayChanged += UpdateMatchDay;
            _view.DataContext = _viewModel;
            #endregion

            _mainWindow.Content = _view;
        }

        private void ExecuteBackCommand(object obj)
        {
            var menuWindow = new MenuWindowController();
            menuWindow.Initialize(_mainWindow, _bettor);
        }
        
        private void UpdateMatchDay(object sender, string s)
        {
            var matchday = _view.MatchDayComboBox.SelectedIndex;
            if (matchday == 0)
            {
                var rankedTeams = _bettorClient.GetAllRankedTeams(_selectedSeason);
                _viewModel.Teams = rankedTeams.ToList();
            }
            else
            {
                var rankedTeams = _bettorClient.GetRankedTeams(_selectedSeason, matchday);
                _viewModel.Teams = rankedTeams.ToList();
            }
        }
    }
}