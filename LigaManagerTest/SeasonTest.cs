using System;
using System.Linq;
using LigaManagerServer.Interfaces;
using LigaManagerServer.Models;
using LigaManagerServer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LigaManagerTest
{
    [TestClass]
    public class SeasonTest
    {
        private readonly IPersistenceService<Season> _seasonService = new PersistenceService<Season>();

        [TestMethod]
        public void GetSeasonsTest()
        {
            var seasons = _seasonService.GetAll();
            if (seasons == null) Assert.Fail();
        }

        [TestMethod]
        public void AddSeasonTest()
        {
            var seasons = _seasonService.GetAll();
            var max = seasons.Max(x => x.Sequence);
            var season = new Season {Name = "Test " + DateTime.Now, Sequence = ++max};
            _seasonService.Add(season);
            seasons = _seasonService.GetAll();
            var find = seasons.Find(x => x.Equals(season)); 
            if (find == null) Assert.Fail();
        }

        [TestMethod]
        public void DeleteSeasonTest()
        {
            var seasons = _seasonService.GetAll();
            var max = seasons.Max(x => x.Sequence);
            var season = new Season { Name = "Test " + DateTime.Now, Sequence = ++max };
            _seasonService.Add(season);
            _seasonService.Delete(season);
            var find = seasons.Find(x => x.Equals(season));
            if (find != null) Assert.Fail();
        }
    }
}