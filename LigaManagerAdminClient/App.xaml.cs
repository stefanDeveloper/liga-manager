using System;
using System.Windows;
using System.Windows.Media.Imaging;
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
            var mainWindow = new MainWindow();
            mainWindow.Icon = BitmapFrame.Create(Application.GetResourceStream(new Uri("Data/Images/soccer.png", UriKind.Relative)).Stream);
            var menuWindow = new MenuWindowController();
            menuWindow.Initialize(mainWindow);
        }
    }
}
