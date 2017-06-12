using LigaManagerBettorClient.ViewModels;
using LigaManagerBettorClient.Views;

namespace LigaManagerBettorClient.Controllers
{
    public class SeasonWindowController
    {
        private SeasonWindow _seasonWindow;
        private SeasonWindowViewModel _seasonWindowViewModel;
        public MainWindow MainWindow { get; set; }
        public void Initialize()
        {
            _seasonWindow = new SeasonWindow();
            _seasonWindowViewModel = new SeasonWindowViewModel();

            _seasonWindow.DataContext = _seasonWindowViewModel;
            MainWindow.Width = 1200;
            MainWindow.Height = 800;
            MainWindow.Content = _seasonWindow;
        }
    }
}