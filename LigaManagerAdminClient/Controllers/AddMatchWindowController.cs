using System;
using System.Collections.Generic;
using System.Windows;
using LigaManagerAdminClient.AdminClientService;
using LigaManagerAdminClient.ViewModels;
using LigaManagerAdminClient.Views;
using LigaManagerBettorClient.Frameworks;

namespace LigaManagerAdminClient.Controllers
{
    public class AddMatchWindowController
    {
        private AddMatchWindow _view;
        private AddMatchWindowViewModel _viewModel;

        public Match Match { get; set; }
        public List<Team> HomeTeams { get; set; }
        public List<Team> AwayTeams { get; set; }


        public Match ShowMatch()
        {   
            #region View and ViewModel
            _view = new AddMatchWindow();
            _viewModel = new AddMatchWindowViewModel
            {
                 SelectedMatch = Match,
                 SelectedAwayTeam = Match.AwayTeam,
                 SelectedHomeTeam = Match.HomeTeam,
                 Minute = Match.DateTime.Minute,
                 Hour = Match.DateTime.Hour,
                 HomeTeams = HomeTeams,
                 AwayTeams = AwayTeams,
                 OkCommand = new RelayCommand(ExecuteOkCommand),
                 CancelCommand = new RelayCommand(ExecuteCancelCommand)
                 
            };
            _view.DataContext = _viewModel;
            _view.ResizeMode = ResizeMode.NoResize;
            #endregion

            return _view.ShowDialog() == true ? _viewModel.SelectedMatch : null;
        }
        #region Execute Commands
        public void ExecuteOkCommand(object obj)
        {
            if (_view.AwayTeamComboBox.SelectedValue == null ||
                _view.HomeTeamComboBox.SelectedValue == null ||
                _view.HourTextBox.Text == string.Empty ||
                _view.MinuteTextBox.Text == string.Empty ||
                _view.HomeTeamScore.Text == string.Empty ||
                _view.AwayTeamScore.Text == string.Empty)
            {
                MessageBox.Show("Spiel konnte nicht hinzugefügt werden, da die Angaben nicht korrekt ausgefüllt sind!", "Hinzufügen fehlgeschlagen",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Check if Home and Away Team is equal.
            if (_viewModel.SelectedHomeTeam.Name.ToUpper().Equals(_viewModel.SelectedAwayTeam.Name.ToUpper()))
            {
                MessageBox.Show("Spiel konnte nicht hinzugefügt werden, da die Mannschaft gegen sich selber spielt!", "Hinzufügen fehlgeschlagen",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Set Match
            Match.AwayTeam = _viewModel.SelectedAwayTeam;
            Match.HomeTeam = _viewModel.SelectedHomeTeam;
            Match.DateTime = new DateTime(Match.DateTime.Year, Match.DateTime.Month, Match.DateTime.Day, _viewModel.Hour, _viewModel.Minute, 0);
            _view.DialogResult = true;
            _view.Close();
        }

        public void ExecuteCancelCommand(object obj)
        {
            _view.DialogResult = false;
            _view.Close();
        }
        #endregion
    }
}