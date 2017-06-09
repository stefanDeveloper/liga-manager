using System.Windows.Input;
using LigaManagerBettorClient.Frameworks;
using PropertyChanged;

namespace LigaManagerBettorClient.ViewModels
{
    public class LoginWindowViewModel : ViewModelBase
    {
        public string Nickname { get; set; }
        public ICommand SignInCommand { get; set; }

    }
}