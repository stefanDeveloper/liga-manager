using System.Windows;
using LigaManagerAdminClient.Controllers;
using LigaManagerAdminClient.Views;

namespace LigaManagerAdminClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var menuWindow = new MenuWindowController();
            menuWindow.Initialize(new MainWindow());
        }
    }
}
