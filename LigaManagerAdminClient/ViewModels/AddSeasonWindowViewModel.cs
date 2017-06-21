using System.Windows.Input;
using LigaManagerAdminClient.AdminClientService;
using LigaManagerAdminClient.Framework;

namespace LigaManagerAdminClient.ViewModels
{
    public class AddSeasonWindowViewModel :ViewModelBase
    {
        public Season Season { get; set; }
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
    }
}