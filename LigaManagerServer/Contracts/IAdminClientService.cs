using System;
using System.ServiceModel;
using System.Xml;
using LigaManagerServer.Models;

namespace LigaManagerServer.Contracts
{
    /// <summary>
    /// This Contract provides all interfaces, which are necessary for the admin client of the LigaManager.
    /// </summary>
    [ServiceContract]
    public interface IAdminClientService : ILigaManagerService
    {
        /// <summary>
        /// Adds an new <see cref="Bettor"/>. If <see cref="Bettor"/> Nickname exists, return false.
        /// </summary>
        /// <param name="bettor"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddBettor(Bettor bettor);
        /// <summary>
        /// Updates a <see cref="Bettor"/>. If <see cref="Bettor"/> Nickname exists, return false.
        /// </summary>
        /// <param name="bettor"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateBettor(Bettor bettor);
        /// <summary>
        /// Deletes <see cref="Bettor"/>.
        /// </summary>
        /// <param name="bettor"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteBettor(Bettor bettor);



        /// <summary>
        /// Adds a new <see cref="Team"/>. If <see cref="Team"/> Name exists, return false.
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddTeam(Team team);
        /// <summary>
        /// Updates a new <see cref="Team"/>. If <see cref="Team"/> Name exists, return false.
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateTeam(Team team);
        /// <summary>
        /// Deletes <see cref="Team"/>
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteTeam(Team team);


        
        /// <summary>
        /// Adds a new <see cref="Season"/>. If <see cref="Season"/> Name exists, return false.
        /// </summary>
        /// <param name="season"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddSeason(Season season);
        /// <summary>
        /// Updates a new <see cref="Season"/>. If <see cref="Season"/> Name exists, return false.
        /// </summary>
        /// <param name="season"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateSeason(Season season);
        /// <summary>
        /// Deletes a <see cref="Season"/>.
        /// </summary>
        /// <param name="season"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteSeason(Season season);



        /// <summary>
        /// Adds a <see cref="Match"/>. If <see cref="Match"/> exists, consider Season, HomeTeam and AwayTeam, return false.
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddMatch(Match match);
        /// <summary>
        /// Updates <see cref="Match"/>. If <see cref="Match"/> exists, consider Season, HomeTeam and AwayTeam, return false.
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateMatch(Match match);
        /// <summary>
        /// Deletes <see cref="Match"/>. Including all <see cref="Bet"/>'s of Match.
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteMatch(Match match);



        /// <summary>
        /// Adds <see cref="SeasonToTeamRelation"/>.
        /// </summary>
        /// <param name="seasonToTeamRelation"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddSeasonToTeamRelation(SeasonToTeamRelation seasonToTeamRelation);
        /// <summary>
        /// Deletes <see cref="SeasonToTeamRelation"/>. Including all Matches and Bets according of SeasonToTeamRelation.
        /// </summary>
        /// <param name="seasonToTeamRelation"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteSeasonToTeamRelation(SeasonToTeamRelation seasonToTeamRelation);



        /// <summary>
        /// Generates Matches. Finds by Beginn <see cref="DateTime"/> and End <see cref="DateTime"/> 
        /// all relevante MatchDays and create matches. If a Match does already exists it will not be added.
        /// </summary>
        /// <param name="season"></param>
        /// <param name="beginDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <returns></returns>
        [OperationContract]
        bool GenerateMatches(Season season, DateTime beginDateTime, DateTime endDateTime);


        /// <summary>
        /// Adds a MatchDay loaded by an XML File. If the File does not match, it'll return false.
        /// If an Match already exists, it will not be added.
        /// </summary>
        /// <param name="xmlElement"></param>
        /// <param name="selectedSeason"></param>
        /// <param name="matchday"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddMatchDay(XmlElement xmlElement, Season selectedSeason, int matchday);
    }
}