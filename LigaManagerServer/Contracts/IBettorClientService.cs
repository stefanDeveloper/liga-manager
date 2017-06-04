using System.ServiceModel;

namespace LigaManagerServer.Contracts
{
    [ServiceContract]
    public interface IBettorClientService : ILigaManagerService
    {
        [OperationContract]
        void Test();
    }
}