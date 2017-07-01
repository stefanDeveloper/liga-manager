using System;
using System.Windows;
using LigaManagerAdminClient.AdminClientService;
using LigaManagerAdminClient.ViewModels;
using LigaManagerAdminClient.Views;
using LigaManagerBettorClient.Frameworks;

namespace LigaManagerAdminClient.Controllers
{
    public class GenerateMatchesWindowController
    {
        private GenerateMatchesWindow _view;
        private GenerateMatchesWindowViewModel _viewModel;

        public Season Season { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool ChooseDateIntervall()
        {
            #region View And ViewModel

            _view = new GenerateMatchesWindow();
            _viewModel = new GenerateMatchesWindowViewModel
            {
                SelectedSeason = Season,
                SelectedBeginDate = BeginDate,
                SelectedEndDate = EndDate,
                CancelCommand = new RelayCommand(ExecuteCancelCommand),
                OkCommand = new RelayCommand(ExecuteOkCommand)
            };
            _view.DataContext = _viewModel;
            _view.ResizeMode = ResizeMode.NoResize;
            _view.ShowDialog();

            return _view.DialogResult != null && (bool) _view.DialogResult;

            #endregion

        }

        #region Commands
        private void ExecuteOkCommand(object o)
        {
            if (_viewModel.SelectedEndDate < _viewModel.SelectedBeginDate)
            {
                MessageBox.Show("Ende ist vor Beginn!", "Nicht zulässig",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Set Model
            BeginDate = _viewModel.SelectedBeginDate;
            EndDate = _viewModel.SelectedEndDate;
            _view.DialogResult = true;
            _view.Close();
        }

        private void ExecuteCancelCommand(object o)
        {
            _view.DialogResult = false;
            _view.Close();
        }
        #endregion
    }
}