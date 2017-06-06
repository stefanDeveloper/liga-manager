using System;
using LigaManagerServer.Interfaces;
using LigaManagerServer.Models;
using LigaManagerServer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LigaManagerTest
{
    [TestClass]
    public class BettorTest
    {
        private readonly IBettorService _bettorService = new BettorService();
        private Bettor _testBettor;
        private Bettor _testBettor2;

        [TestInitialize]
        public void InitTest()
        {
            _testBettor = new Bettor
            {
                Nickname = "Juergen173",
                Firstname = "Jürgen",
                Lastname = "Eisele"
            };

            _testBettor2 = new Bettor
            {
                Nickname = "Test" + DateTime.Now,
                Lastname = "Test",
                Firstname = "Test"
            };
        }

        [TestMethod]
        public void GetBettorTest()
        {
            var bettor = _bettorService.GetBettor("Juergen173");
            if (!bettor.Equals(_testBettor))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetAllBettorsTest()
        {
            var bettor = _bettorService.GetBettors();
            if (bettor == null)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void AddBettorTest()
        {
            var isSuccess = _bettorService.AddBettor(_testBettor2);
            if (!isSuccess) Assert.Fail();
            var bettor = _bettorService.GetBettor(_testBettor2.Nickname);
            if (!bettor.Equals(_testBettor2))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void EqualBettorTest()
        {
            var isSuccess = _bettorService.AddBettor(_testBettor);
            if (isSuccess) Assert.Fail();
        }
    }
}
