using System.Collections.Generic;
using LigaManagerServer.Models;

namespace LigaManagerServer.Interfaces
{
    public interface ITeamService
    {
        List<Team> GetTeams();

        bool AddTeam(Team team);

        bool DeleteTeam(Team team);

        bool ChangeTeam(Team team);

        List<SeasonToTeamRelation> GetSeasonToTeamRelations();

        bool AddSeasonToTeam(Team team, Season season);

        bool DeleteSeasonToTeam(Team team, Season season);
    }
}