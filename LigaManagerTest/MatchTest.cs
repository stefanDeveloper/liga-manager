using LigaManagerServer.Interfaces;
using LigaManagerServer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LigaManagerTest
{
    [TestClass]
    public class MatchTest
    {
        private readonly IMatchService _matchService = new MatchService();
        [TestMethod]
        public void GetMatchesTest()
        {
            var matches = _matchService.GetMatches();
            if (matches == null) Assert.Fail();
        }
    }
}