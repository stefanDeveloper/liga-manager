using LigaManagerAdminClient.ViewModels;
using LigaManagerAdminClient.Views;
using LigaManagerBettorClient.Frameworks;
using LigaManagerServer.Models;

namespace LigaManagerAdminClient.Controllers
{
    public class AddBettorWindowController
    {
        private AddBettorWindow _view;
        private AddBettorWindowViewModel _viewModel;

        public Bettor Bettor { get; set; }

        public Bettor ShowBettor()
        {
            #region View and ViewModel
            _view = new AddBettorWindow();
            _viewModel = new AddBettorWindowViewModel
            {
                OkCommand = new RelayCommand(ExecuteOkCommand),
                CancelCommand = new RelayCommand(ExecuteCancelCommand)
            };
            #endregion

            return _view.ShowDialog() == true ? _viewModel.Bettor : null;
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