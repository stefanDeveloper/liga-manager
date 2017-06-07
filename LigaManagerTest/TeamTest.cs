using System;
using LigaManagerServer.Interfaces;
using LigaManagerServer.Models;
using LigaManagerServer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LigaManagerTest
{
    [TestClass]
    public class TeamTest
    {

        private readonly IPersistenceService<Team> _teamService = new PersistenceService<Team>();
        private readonly IPersistenceService<SeasonToTeamRelation> _seasonToTeamRelationService = new PersistenceService<SeasonToTeamRelation>();

        [TestMethod]
        public void GetTeamsTest()
        {
            var teams = _teamService.GetAll();
            if (teams == null) Assert.Fail();
        }

        [TestMethod]
        public void RemoveTeamTest()
        {
            var team = new Team {Name = "Test " + DateTime.Now};
            _teamService.Add(team);
            var isDeleted = _teamService.Delete(team);
            if (!isDeleted) Assert.Fail();
        }

        [TestMethod]
        public void AddTeamTest()
        {
            var addTeam = _teamService.Add(new Team {Name = "Test " + DateTime.Now});
            if (!addTeam) Assert.Fail();

        }

        [TestMethod]
        public void GetSeasonsToTeamRelationTest()
        {
            var seasonToTeamRelations = _seasonToTeamRelationService.GetAll();
            if (seasonToTeamRelations == null) Assert.Fail();
        }
    }
}