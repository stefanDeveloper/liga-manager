using System.ServiceModel;

namespace LigaManagerServer.Contracts
{
    [ServiceContract]
    public interface ILigaManagerService
    {
        [OperationContract]
        void Test1();
    }
}