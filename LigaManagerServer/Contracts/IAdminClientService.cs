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
        bool AddTeam(Team team);
        [OperationContract]
        bool UpdateTeam(Team team);
        [OperationContract]
        bool DeleteTeam(Team team);

        [OperationContract]
        bool DeleteSeason(Season season);
        [OperationContract]
        bool AddSeason(Season season);
        [OperationContract]
        bool UpdateSeason(Season season);

        [OperationContract]
        bool DeleteMatch(Match match);
        [OperationContract]
        bool AddMatch(Match match);
        [OperationContract]
        bool UpdateMatch(Match match);

        [OperationContract]
        bool AddSeasonToTeamRelation(SeasonToTeamRelation seasonToTeamRelation);
        [OperationContract]
        bool DeleteSeasonToTeamRelation(SeasonToTeamRelation seasonToTeamRelation);

        [OperationContract]
        void GenerateMatches();
    }
}