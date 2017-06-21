using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.Windows;
using System.Windows.Data;
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
                 DateTime = Match.DateTime,
                 SelectedAwayTeam = Match.AwayTeam,
                 SelectedHomeTeam = Match.HomeTeam,
                 HomeTeams = HomeTeams,
                 AwayTeams = AwayTeams,
                 OkCommand = new RelayCommand(ExecuteOkCommand),
                 CancelCommand = new RelayCommand(ExecuteCancelCommand)
                 
            };
            _view.TimeControl.DateTimeValue = Match.DateTime;
            _view.DataContext = _viewModel;
            #endregion

            return _view.ShowDialog() == true ? _viewModel.SelectedMatch : null;
        }

        public void ExecuteOkCommand(object obj)
        {
            if (_view.AwayTeamComboBox.SelectedValue == null ||
                _view.HomeTeamComboBox.SelectedValue == null)
            {
                MessageBox.Show("Spiel konnte nicht hinzugefügt werden, da die Angaben nicht korrekt ausgefüllt sind!", "Hinzufügen fehlgeschlagen",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Match.AwayTeam = _viewModel.SelectedAwayTeam;
            Match.HomeTeam = _viewModel.SelectedHomeTeam;
            Match.DateTime = new DateTime(year: _viewModel.DateTime.Year, month: _viewModel.DateTime.Month, day: _viewModel.DateTime.Day
                , hour: _view.TimeControl.DateTimeValue.Value.Hour, minute: _view.TimeControl.DateTimeValue.Value.Minute, second: _view.TimeControl.DateTimeValue.Value.Second);

            _view.DialogResult = true;
            _view.Close();
        }

        public void ExecuteCancelCommand(object obj)
        {
            _view.DialogResult = false;
            _view.Close();
        }
    }
}