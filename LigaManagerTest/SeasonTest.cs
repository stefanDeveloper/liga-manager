using LigaManagerServer.Interfaces;
using LigaManagerServer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LigaManagerTest
{
    [TestClass]
    public class SeasonTest
    {
        private ISeasonService _seasonService = new SeasonService();

        [TestMethod]
        public void GetSeasonsTest()
        {
            var seasons = _seasonService.GetSeasons();
            if (seasons == null) Assert.Fail();
        }
    }
}