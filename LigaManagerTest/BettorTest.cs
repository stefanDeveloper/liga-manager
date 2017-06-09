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
        private readonly PersistenceService<Bettor> _persistenceService = new PersistenceService<Bettor>();
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
                Nickname = "Test " + DateTime.Now,
                Lastname = "Test",
                Firstname = "Test"
            };
        }

        [TestMethod]
        public void GetBettorTest()
        {
            var bettor = _persistenceService.Get(1);
            if (!bettor.Equals(_testBettor))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetAllBettorsTest()
        {
            var bettor = _persistenceService.GetAll();
            if (bettor == null)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void AddBettorTest()
        {
            var isSuccess = _persistenceService.Add(_testBettor2);
            if (!isSuccess) Assert.Fail();
            var bettors = _persistenceService.GetAll();
            var bettor = bettors.Find(x => x.Nickname.Equals(_testBettor2.Nickname));
            if (!bettor.Equals(_testBettor2))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void EqualBettorTest()
        {
            var isSuccess = _persistenceService.Add(_testBettor);
            if (isSuccess) Assert.Fail();
        }

        [TestMethod]
        public void ChangeBettorTest()
        {
            var bettor = _persistenceService.Get(9);
            bettor.Nickname = "Juergen173 " + DateTime.Now;
            var isSuccess = _persistenceService.Update(bettor);
            if (!isSuccess) Assert.Fail();
        }
    }
}
