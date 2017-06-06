using System.Collections.Generic;
using LigaManagerServer.Framework;
using LigaManagerServer.Interfaces;
using LigaManagerServer.Models;

namespace LigaManagerServer.Services
{
    public class TeamService : ITeamService
    {
        private readonly Repository<Team> _teamRepository = new Repository<Team>();
        private readonly Repository<SeasonToTeamRelation> _seasonToTeamRelationRepository = new Repository<SeasonToTeamRelation>();
        private static readonly object StaticLock = new object();

        public List<Team> GetTeams()
        {
            lock (StaticLock)
            {
                var teams = _teamRepository.GetAll();
                return teams;
            }
        }

        public bool AddTeam(Team team)
        {
            lock (StaticLock)
            {
                var teams = _teamRepository.GetAll();
                var find = teams.Find(x => x.Name.ToUpper().Equals(team.Name.ToUpper()));
                if (find != null) return false;
                _teamRepository.Save(team);
                return true;
            }
        }

        public bool DeleteTeam(Team team)
        {
            lock (StaticLock)
            {
                var teams = _teamRepository.GetAll();
                var find = teams.Find(x => x.Equals(team));
                if (find == null) return false;
                _teamRepository.Delete(find);
                return true;
            }
        }

        public bool ChangeTeam(Team team)
        {
            lock (StaticLock)
            {
                return true;
            }
        }

        public List<SeasonToTeamRelation> GetSeasonToTeamRelations()
        {
            lock (StaticLock)
            {
                var seasonToTeamRelations = _seasonToTeamRelationRepository.GetAll();
                return seasonToTeamRelations;
            }
        }

        public bool AddSeasonToTeam(Team team, Season season)
        {
            lock (StaticLock)
            {
                if (team == null) return false;
                if (season == null) return false;
                var seasonToTeamRelation = new SeasonToTeamRelation { Team = team, Season = season };

                var seasonToTeamRelations = _seasonToTeamRelationRepository.GetAll();
                var find = seasonToTeamRelations.Find(x => x.Equals(seasonToTeamRelation));
                if (find != null) return false;

                _seasonToTeamRelationRepository.Save(seasonToTeamRelation);
                return true;
            }
        }

        public bool DeleteSeasonToTeam(Team team, Season season)
        {
            lock (StaticLock)
            {
                if (team == null) return false;
                if (season == null) return false;
                var seasonToTeamRelation = new SeasonToTeamRelation { Team = team, Season = season };
                var seasonToTeamRelations = _seasonToTeamRelationRepository.GetAll();
                var find = seasonToTeamRelations.Find(x => x.Equals(seasonToTeamRelation));
                
                _seasonToTeamRelationRepository.Delete(find);
                return true;
            }
        }
    }
}