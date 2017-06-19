using System.Collections.Generic;
using System.ServiceModel;
using LigaManagerServer.Models;

namespace LigaManagerServer.Contracts
{
    /// <summary>
    /// Services to get all general data. With this service no data can be changed or deleted.
    /// </summary>
    [ServiceContract]
    public interface ILigaManagerService
    {
        /// <summary>
        /// Checks the connection.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool IsOpen();

        [OperationContract]
        List<Match> GetMatches(Season season);

        [OperationContract]
        List<Bet> GetBets(Bettor bettor);

        [OperationContract]
        List<Bet> GetAllBets();

        [OperationContract]
        List<Bettor> GetBettors();

        [OperationContract]
        Bettor GetBettor(string nickname);

        [OperationContract]
        List<Season> GetSeasons();

        [OperationContract]
        List<SeasonToTeamRelation> GetTeams(Season season);

        [OperationContract]
        List<SeasonToTeamRelation> GetAllTeams();
    }
}