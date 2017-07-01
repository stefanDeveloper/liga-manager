using System.Windows;
using LigaManagerAdminClient.AdminClientService;
using LigaManagerAdminClient.ViewModels;
using LigaManagerAdminClient.Views;
using LigaManagerBettorClient.Frameworks;

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
                CancelCommand = new RelayCommand(ExecuteCancelCommand),
                Bettor = Bettor
            };
            _view.DataContext = _viewModel;
            _view.ResizeMode = ResizeMode.NoResize;
            #endregion

            return _view.ShowDialog() == true ? _viewModel.Bettor : null;
        }

        #region Execute Commands
        public void ExecuteOkCommand(object obj)
        {
            if (_view.FirstnameTextBox.Text.Equals(string.Empty) ||
                _view.LastnameTextBox.Text.Equals(string.Empty) ||
                _view.NicknameTextBox.Text.Equals(string.Empty))
            {
                MessageBox.Show("Tipper konnte nicht hinzugefügt werden, da entweder der Vorname, Nachname oder Nickname nicht ausgefüllt ist!", "Hinzufügen fehlgeschlagen",
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