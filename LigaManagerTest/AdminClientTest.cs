using System;
using System.Linq;
using LigaManagerTest.AdminService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LigaManagerTest
{
    [TestClass]
    public class AdminClientTest
    {
        [TestMethod]
        public void GenerateMatchesTest()
        {
            var adminClient = new AdminClientServiceClient();
            adminClient.GenerateMatches(adminClient.GetSeasons().First(), DateTime.Now, DateTime.Now.AddDays(180));
        }
    }
}
