using LigaManagerServer.Interfaces;
using LigaManagerServer.Models;
using LigaManagerServer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LigaManagerTest
{
    [TestClass]
    public class BetTest
    {
        private readonly IPersistenceService<Bet> _betService = new PersistenceService<Bet>();
        [TestMethod]
        public void GetBetsTest()
        {
            var bets = _betService.GetAll();
            if (bets == null) Assert.Fail();
        }
    }
}