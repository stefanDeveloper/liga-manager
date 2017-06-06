using System;
using System.Collections.Generic;
using System.IO;
using LigaManagerServer.Framework;
using LigaManagerServer.Interfaces;
using LigaManagerServer.Models;

namespace LigaManagerServer.Services
{
    public class BettorService : IBettorService
    {
        private readonly Repository<Bettor> _repository = new Repository<Bettor>();
        private static readonly object StaticLock = new object();

        public Bettor GetBettor(string name)
        {
            lock (StaticLock)
            {
                var bettors = _repository.GetAll();
                return bettors.Find(x => x.Nickname.Equals(name));
            }
        }

        public bool AddBettor(Bettor bettor)
        {
            lock (StaticLock)
            {
                var bettors = _repository.GetAll();
                var find = bettors.Find(x => x.Nickname.Equals(bettor.Nickname));
                if (find != null) return false;
                _repository.Save(bettor);
                return true;
            }
        }

        public bool DeleteBettor(Bettor bettor)
        {
            lock (StaticLock)
            {
                var bettors = _repository.GetAll();
                var find = bettors.Find(x => x.Equals(bettor));
                if (find == null) return false;
                _repository.Delete(find);
                return true;
            }
        }

        public List<Bettor> GetBettors()
        {
            lock (StaticLock)
            {
                var bettors = _repository.GetAll();
                return bettors;
            }
        }
    }
}