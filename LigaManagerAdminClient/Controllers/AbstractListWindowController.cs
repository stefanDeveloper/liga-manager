using System.Windows;
using LigaManagerAdminClient.Views;

namespace LigaManagerAdminClient.Controllers
{
    public abstract class AbstractListWindowController
    {
        public MainWindow MainWindow;

        public abstract void Initialize(MainWindow mainWindow);

        protected void ExecuteBackCommand(object obj)
        {
            var menuWindow = new MenuWindowController();
            menuWindow.Initialize(MainWindow);
        }

        protected abstract void ExecuteAddCommand(object obj);


        protected abstract void ExecuteChangeCommand(object obj);


        protected abstract void ExecuteDeleteCommand(object obj);


        protected abstract void ReloadModels();


        protected void UpdateModels(bool isUpdated, string errorMessage, string errorHeader)
        {
            if (isUpdated)
            {
                ReloadModels();
            }
            else
            {
                MessageBox.Show(errorMessage, errorHeader, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}