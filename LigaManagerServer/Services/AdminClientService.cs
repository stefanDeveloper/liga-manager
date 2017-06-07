using System;
using LigaManagerServer.Contracts;
using LigaManagerServer.Interfaces;
using LigaManagerServer.Models;

namespace LigaManagerServer.Services
{
    public class AdminClientService : IAdminClientService
    {
        private static readonly object StaticLock = new object();

        public void GetMatches(Season season)
        {
            lock (StaticLock)
            {

            }
        }

        public bool AddBettor(Bettor bettor)
        {
            lock (StaticLock)
            {
                return true;
            }
        }

        public bool UpdateBettor(Bettor bettor)
        {
            lock (StaticLock)
            {
                return true;
            }
        }

        public bool DeleteBettor(Bettor bettor)
        {
            lock (StaticLock)
            {
                return true;
            }
        }

        public bool DeleteSeason(Season season)
        {
            lock (StaticLock)
            {
                return true;
            }
        }

        public bool AddSeason(Season season)
        {
            lock (StaticLock)
            {
                return true;
            }
        }

        public bool UpdateSeason(Season season)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteMatch(Match season)
        {
            throw new System.NotImplementedException();
        }

        public bool AddMatch(Match season)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateMatch(Match season)
        {
            throw new System.NotImplementedException();
        }

        public void GenerateMatches()
        {
            throw new System.NotImplementedException();
        }
    }
}