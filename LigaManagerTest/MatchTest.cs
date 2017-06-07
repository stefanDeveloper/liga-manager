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
    }
}