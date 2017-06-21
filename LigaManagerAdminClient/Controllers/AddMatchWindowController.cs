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

        public Match Bettor { get; set; }

        public Match ShowBettor()
        {
            #region View and ViewModel
            _view = new AddMatchWindow();
            _viewModel = new AddMatchWindowViewModel
            {
                
            };
            _view.DataContext = _viewModel;
            #endregion

            return _view.ShowDialog() == true ? _viewModel.Match : null;
        }

        public void ExecuteOkCommand(object obj)
        {
            /*if (_view.FirstnameTextBox.Text.Equals(string.Empty) ||
                _view.LastnameTextBox.Text.Equals(string.Empty) ||
                _view.NicknameTextBox.Text.Equals(string.Empty))
            {
                MessageBox.Show("Tipper konnte nicht hinzugefügt werden, da entweder der Vorname, Nachname oder Nickname nicht ausgefüllt ist!", "Hinzufügen fehlgeschlagen",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }*/
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