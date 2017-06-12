using System.ServiceModel;
using LigaManagerServer.Models;

namespace LigaManagerServer.Contracts
{
    [ServiceContract]
    public interface IBettorClientService : ILigaManagerService
    {
        [OperationContract]
        bool IsValidNickname(string name);

        [OperationContract]
        bool AddBet(Bet bet);
        [OperationContract]
        bool ChangeBet(Bet bet);
        [OperationContract]
        Bet GetBet(Match match, Bettor bettor);
    }
}