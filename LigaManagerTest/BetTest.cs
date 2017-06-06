using LigaManagerServer.Interfaces;
using LigaManagerServer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LigaManagerTest
{
    [TestClass]
    public class BetTest
    {
        private readonly IBetService _betService = new BetService();
        [TestMethod]
        public void GetBetsTest()
        {
            var bets = _betService.GetBets();
            if (bets == null) Assert.Fail();
        }
    }
}