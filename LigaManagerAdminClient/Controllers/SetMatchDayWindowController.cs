using System;
using System.Windows;
using LigaManagerAdminClient.ViewModels;
using LigaManagerAdminClient.Views;
using LigaManagerBettorClient.Frameworks;

namespace LigaManagerAdminClient.Controllers
{
    public class SetMatchDayWindowController
    {
        private SetMatchDayWindow _view;
        private SetMatchDayWindowViewModel _viewModel;
        public int MatchDay { get; set; }
        public int SetMatchDay()
        {
            #region View and ViewModel
            _view = new SetMatchDayWindow();
            _viewModel = new SetMatchDayWindowViewModel
            {
                OkCommand = new RelayCommand(ExecuteOkCommand),
                CancelCommand = new RelayCommand(ExecuteCancelCommand),
                MatchDay = MatchDay

            };
            _view.DataContext = _viewModel;
            #endregion

            return _view.ShowDialog() == true ? _viewModel.MatchDay : -1;
        }
        #region Execute Commands
        public void ExecuteOkCommand(object obj)
        {
            if (_view.MatchDayTextBox.Text.Equals(string.Empty))
            {
                MessageBox.Show("Spieltag ist leer!", "Kein Spieltag ausgewählt",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _view.DialogResult = true;
                _view.Close();
            }
            
        }

        public void ExecuteCancelCommand(object obj)
        {
            _view.DialogResult = false;
            _view.Close();
        }
        #endregion
    }
}