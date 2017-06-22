using System.Collections.Generic;
using System.ServiceModel;
using LigaManagerServer.Models;

namespace LigaManagerServer.Contracts
{
    /// <summary>
    /// Services for the BettorClient.
    /// </summary>
    [ServiceContract]
    public interface IBettorClientService : ILigaManagerService
    {
        /// <summary>
        /// Checks if a nickname exists. Lower of upper case is considered.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [OperationContract]
        bool IsValidNickname(string name);
        /// <summary>
        /// Adds a bet. Consider that it's only available half an hour before the match.
        /// </summary>
        /// <param name="bet"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddBet(Bet bet);
        /// <summary>
        /// Changes a Bet. Consider that it's only available half an hour before the match.
        /// </summary>
        /// <param name="bet"></param>
        /// <returns></returns>
        [OperationContract]
        bool ChangeBet(Bet bet);
        /// <summary>
        /// Removes a Bet. Consider that it's only available half an hour before the match.
        /// </summary>
        /// <param name="bet"></param>
        /// <returns></returns>
        [OperationContract]
        bool RemoveBet(Bet bet);
        /// <summary>
        /// Returns a bet of a specific match of an bettor.
        /// </summary>
        /// <param name="match"></param>
        /// <param name="bettor"></param>
        /// <returns></returns>
        [OperationContract]
        Bet GetBet(Match match, Bettor bettor);

        /// <summary>
        /// Returns all <see cref="RankedBettor"/> of a Season.
        /// </summary>
        /// <param name="season"><see cref="Season"/></param>
        /// <returns></returns>
        [OperationContract]
        List<RankedBettor> GetAllRankedBettors(Season season);

        /// <summary>
        /// Returns all <see cref="RankedBettor"/> of a the specific matchday. 
        /// </summary>
        /// <param name="season"><see cref="Season"/></param>
        /// <param name="matchday"></param>
        /// <returns></returns>
        [OperationContract]
        List<RankedBettor> GetRankedBettors(Season season, int matchday);

        /// <summary>
        /// Returns all <see cref="RankedTeam"/> of a Season. The score is calculate by all matches of a team. 
        /// If a team win a match, it gain 3 points. If a team loose a match, it gains 0. 
        /// And if a team tied a match, booth gain 1 point.
        /// </summary>
        /// <param name="season"></param>
        /// <returns></returns>
        [OperationContract]
        List<RankedTeam> GetAllRankedTeams(Season season);

        /// <summary>
        /// Returns all <see cref="RankedTeam"/> of a specific matchday.
        /// </summary>
        /// <param name="season"></param>
        /// <param name="matchday"></param>
        /// <returns></returns>
        [OperationContract]
        List<RankedTeam> GetRankedTeams(Season season, int matchday);
    }
}