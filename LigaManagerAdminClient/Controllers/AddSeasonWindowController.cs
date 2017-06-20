using LigaManagerAdminClient.AdminClientService;
using LigaManagerAdminClient.ViewModels;
using LigaManagerAdminClient.Views;
using LigaManagerBettorClient.Frameworks;
using LigaManagerServer.Models;

namespace LigaManagerAdminClient.Controllers
{
    public class AddSeasonWindowController
    {
        private AddSeasonWindow _view;
        private AddSeasonWindowViewModel _viewModel;

        public Season Season { get; set; }  

        public Season ShowSeason()
        {
            #region View And ViewModel
            _view = new AddSeasonWindow();
            _viewModel = new AddSeasonWindowViewModel
            {
                Season = Season,
                OkCommand = new RelayCommand(ExecuteOkCommand),
                CancelCommand = new RelayCommand(ExecuteCancelCommand),
            };
            _view.DataContext = _viewModel;
            #endregion

            return _view.ShowDialog() == true ? _viewModel.Season : null;
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