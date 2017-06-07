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
    }
}