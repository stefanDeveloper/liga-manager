using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using LigaManagerBettorClient.Controllers;
using LigaManagerBettorClient.Views;

namespace LigaManagerBettorClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var loginWindow = new LoginWindowController();
            loginWindow.Initialize();
        }
    }
}
