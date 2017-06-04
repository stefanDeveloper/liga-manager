using System.ServiceModel;

namespace LigaManagerServer.Contracts
{
    [ServiceContract]
    public interface IAdminClientService : ILigaManagerService
    {
        [OperationContract]
        void Test();
    }
}