using System.ServiceModel;
using LigaManagerServer.Models;

namespace LigaManagerServer.Contracts
{
    [ServiceContract]
    public interface IBettorClientService : ILigaManagerService
    {
        [OperationContract]
        bool Login(string name);

        [OperationContract]
        bool AddBet(Bet bet);
    }
}