using System.Collections.Generic;
using LigaManagerAdminClient.ViewModels;
using LigaManagerAdminClient.Views;
using LigaManagerBettorClient.Frameworks;
using LigaManagerServer.Models;

namespace LigaManagerAdminClient.Controllers
{
    public class AddTeamWindowController
    {
        private AddTeamWindow _view;
        private AddTeamWindowViewModel _viewModel;

        public Team Team { get; set; }
        public List<Season> Seasons { get; set; }

        public Team ShowTeam()
        {
            #region View and ViewModel
            _view = new AddTeamWindow();
            _viewModel = new AddTeamWindowViewModel()
            {
                OkCommand = new RelayCommand(ExecuteOkCommand),
                CancelCommand = new RelayCommand(ExecuteCancelCommand),
            };
            _view.DataContext = _viewModel;
            #endregion

            return _view.ShowDialog() == true ? _viewModel.Team : null;
        }

        public void ExecuteOkCommand(object obj)
        {
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