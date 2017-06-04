using System;
using LigaManagerServer.Interfaces;
using LigaManagerServer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LigaManagerTest
{
    [TestClass]
    public class BettorTest
    {
        private BettorService _bettorService = new BettorService();
        [TestMethod]
        public void TestMethod1()
        {
            var bettor = _bettorService.GetBettor("Juergen173");
            var bettorFirstname = bettor.Firstname;
        }
    }
}
