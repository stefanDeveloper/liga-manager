using System.Windows.Input;
using LigaManagerBettorClient.Frameworks;

namespace LigaManagerAdminClient.ViewModels
{
    public class MenuWindowViewModel : ViewModelBase
    {
        public ICommand BettorCommand { get; set; }
        public ICommand TeamCommand { get; set; }
        public ICommand SeasonCommand { get; set; }   
    }
}
