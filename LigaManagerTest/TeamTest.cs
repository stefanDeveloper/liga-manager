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

        private readonly ITeamService _teamService = new TeamService();

        [TestMethod]
        public void GetTeamsTest()
        {
            var teams = _teamService.GetTeams();
            if (teams == null) Assert.Fail();
        }

        [TestMethod]
        public void RemoveTeamTest()
        {
            var isDeleted = _teamService.DeleteTeam(new Team {Name = "FC Bayern München"});
            if (!isDeleted) Assert.Fail();
        }

        [TestMethod]
        public void AddTeamTest()
        {
            var addTeam = _teamService.AddTeam(new Team {Name = "Test " + DateTime.Now});
            if (!addTeam) Assert.Fail();

        }

        [TestMethod]
        public void GetSeasonsToTeamRelationTest()
        {
            var seasonToTeamRelations = _teamService.GetSeasonToTeamRelations();
            if (seasonToTeamRelations == null) Assert.Fail();
        }
    }
}