using System;
using System.Linq;
using LigaManagerServer.Interfaces;
using LigaManagerServer.Models;
using LigaManagerServer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LigaManagerTest
{
    [TestClass]
    public class MatchTest
    {
        private readonly IPersistenceService<Match> _matchService = new PersistenceService<Match>();
        [TestMethod]
        public void GetMatchesTest()
        {
            var matches = _matchService.GetAll();
            if (matches == null) Assert.Fail();
        }

        [TestMethod]
        public void DeleteMatchesTest()
        {
            var matches = _matchService.GetAll();
            var isDeleted = _matchService.Delete(matches.First());
            matches = _matchService.GetAll();
            Assert.IsTrue(isDeleted);
        }

        [TestMethod]
        public void AddMatchesTest()
        {
            var matches = _matchService.GetAll();
            var match = matches.First();
            _matchService.Delete(match);
            match.Id = 0;
            match.DateTime = DateTime.Now.AddHours(30);
            match.Season.Sequence = 100;
            var isDeleted = _matchService.Add(match);
            matches = _matchService.GetAll();
            Assert.IsTrue(isDeleted);
        }
    }
}