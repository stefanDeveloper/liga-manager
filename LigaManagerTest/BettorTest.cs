using LigaManagerServer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LigaManagerTest
{
    [TestClass]
    public class BettorTest
    {
        private readonly BettorService _bettorService = new BettorService();

        [TestMethod]
        public void GetBettorTest()
        {
            var bettor = _bettorService.GetBettor("Juergen173");
        }
    }
}
