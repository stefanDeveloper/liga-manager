using System.ServiceModel;
using LigaManagerServer.Models;

namespace LigaManagerServer.Contracts
{
    /// <summary>
    /// This Contract provides all interfaces, which are necessary for the admin client of the LigaManager.
    /// </summary>
    [ServiceContract]
    public interface IAdminClientService : ILigaManagerService
    {
        [OperationContract]
        bool AddBettor(Bettor bettor);
        [OperationContract]
        bool UpdateBettor(Bettor bettor);
        [OperationContract]
        bool DeleteBettor(Bettor bettor);

        [OperationContract]
        bool DeleteSeason(Season season);
        [OperationContract]
        bool AddSeason(Season season);
        [OperationContract]
        bool UpdateSeason(Season season);

        [OperationContract]
        bool DeleteMatch(Match season);
        [OperationContract]
        bool AddMatch(Match season);
        [OperationContract]
        bool UpdateMatch(Match season);

        void GenerateMatches();
    }
}