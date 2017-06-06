using System;
using System.Collections.Generic;
using System.IO;
using LigaManagerServer.Framework;
using LigaManagerServer.Interfaces;
using LigaManagerServer.Models;
using NHibernate.Mapping;

namespace LigaManagerServer.Services
{
    public class TeamService : ITeamService
    {
        private readonly Repository<Team> _repository = new Repository<Team>();
        private static readonly object StaticLock = new object();

        public List<Team> GetTeams()
        {
            lock (StaticLock)
            {
                var teams = _repository.GetAll();
                return teams;
            }
        }

        public bool AddTeam(Team team)
        {
            lock (StaticLock)
            {
                _repository.Save(team);
                return true;
            }
        }

        public bool DeleteTeam(Team team)
        {
            lock (StaticLock)
            {
                var teams = _repository.GetAll();
                var find = teams.Find(x => x.Equals(team));
                if (find == null) return false;
                _repository.Delete(find);
                return true;
            }
        }
    }
}