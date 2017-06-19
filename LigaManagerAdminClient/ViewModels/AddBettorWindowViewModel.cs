using System.Windows.Input;
using LigaManagerBettorClient.Frameworks;
using Bettor = LigaManagerServer.Models.Bettor;

namespace LigaManagerAdminClient.ViewModels
{
    public class AddBettorWindowViewModel : ViewModelBase
    {
        public Bettor Bettor { get; set; }  
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
    }
}