using System;
using System.Linq;
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
        private readonly IPersistenceService<Match> _matchService = new PersistenceService<Match>();
        [TestMethod]
        public void GetBetsTest()
        {
            var bets = _betService.GetAll();
            if (bets == null) Assert.Fail();
        }

        [TestMethod]
        public void AddBetsTest()
        {
            var bets = _betService.GetAll();
            var match = _matchService.Get(299);
            var bet = bets.First();
            var newBet = new Bet
            {
                Match = match,
                DateTime = DateTime.Now,
                Bettor = bet.Bettor,
                AwayTeamScore = 2,
                HomeTeamScore = 3
            };
            /*var findAll = bets.Find(x => x.Match.Equals(match) && x.Bettor.Equals(bet.Bettor));
            _betService.Delete(findAll);
            bet.DateTime = DateTime.Now;*/
            var isAdded = _betService.Add(newBet);
            Assert.IsTrue(isAdded);
        }
    }
}