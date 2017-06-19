using System.Windows.Input;
using LigaManagerAdminClient.AdminClientService;
using LigaManagerBettorClient.Frameworks;

namespace LigaManagerAdminClient.ViewModels
{
    public class AddBettorWindowViewModel : ViewModelBase
    {
        public Bettor Bettor { get; set; }  
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
    }
}