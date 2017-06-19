using LigaManagerAdminClient.ViewModels;
using LigaManagerAdminClient.Views;

namespace LigaManagerAdminClient.Controllers
{
    public class TeamWindowController
    {
        private TeamWindow _view;
        private TeamWindowViewModel _viewModel;
        private MainWindow _mainWindow;

        public void Initialize(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _view = new TeamWindow();
            _viewModel = new TeamWindowViewModel
            {
                
            };

            _view.DataContext = _viewModel;

            _mainWindow.Content = _view;
        }
    }
}