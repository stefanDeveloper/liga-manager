using System.Windows.Input;
using LigaManagerAdminClient.Framework;
using LigaManagerServer.Models;

namespace LigaManagerAdminClient.ViewModels
{
    public class AddSeasonWindowViewModel :ViewModelBase
    {
        public Season Season { get; set; }
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
    }
}