using System.Collections.Generic;
using System.ServiceModel;
using LigaManagerServer.Models;

namespace LigaManagerServer.Contracts
{
    [ServiceContract]
    public interface ILigaManagerService
    {
        [OperationContract]
        List<Match> GetMatches(Season season);

        [OperationContract]
        List<Bet> GetBets(Bettor bettor);

        [OperationContract]
        List<Bettor> GetBettors();

        [OperationContract]
        Bettor GetBettor(string nickname);

        [OperationContract]
        List<Season> GetSeasons();
    }
}