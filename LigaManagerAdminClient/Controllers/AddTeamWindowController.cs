using System.Collections.Generic;
using System.Windows;
using LigaManagerAdminClient.AdminClientService;
using LigaManagerAdminClient.Models;
using LigaManagerAdminClient.ViewModels;
using LigaManagerAdminClient.Views;
using LigaManagerBettorClient.Frameworks;

namespace LigaManagerAdminClient.Controllers
{
    public class AddTeamWindowController
    {
        private AddTeamWindow _view;
        private AddTeamWindowViewModel _viewModel;

        public Team Team { get; set; }
        public List<SeasonCheckBox> Seasons { get; set; }

        public Team ShowTeam()
        {
            #region View and ViewModel
            _view = new AddTeamWindow();
            _viewModel = new AddTeamWindowViewModel()
            {
                OkCommand = new RelayCommand(ExecuteOkCommand),
                CancelCommand = new RelayCommand(ExecuteCancelCommand),
                Seasons = Seasons,
                Team = Team
            };
            _view.DataContext = _viewModel;
            _view.ResizeMode = ResizeMode.NoResize;
            #endregion

            return _view.ShowDialog() == true ? _viewModel.Team : null;
        }
        #region Execute Commands
        public void ExecuteOkCommand(object obj)
        {
            if (_view.NameTextBox.Text.Equals(string.Empty))
            {
                MessageBox.Show("Mannschaft konnte nicht hinzugefügt werden, da der Name nicht ausgefüllt ist!", "Hinzufügen fehlgeschlagen",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
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