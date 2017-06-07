using System.ServiceModel;
using LigaManagerServer.Models;

namespace LigaManagerServer.Contracts
{
    [ServiceContract]
    public interface ILigaManagerService
    {
        [OperationContract]
        void GetMatches(Season season);
    }
}