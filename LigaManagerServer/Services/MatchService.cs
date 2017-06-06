using System.Collections.Generic;
using LigaManagerServer.Framework;
using LigaManagerServer.Interfaces;
using LigaManagerServer.Models;

namespace LigaManagerServer.Services
{
    public class MatchService : IMatchService
    {
        private readonly Repository<Match> _repository = new Repository<Match>();
        private static readonly object StaticLock = new object();

        public List<Match> GetMatches()
        {
            lock (StaticLock)
            {
                var matches = _repository.GetAll();
                return matches;
            }
        }
        public bool AddMatch(Match match)
        {
            lock (StaticLock)
            {
                _repository.Save(match);
                return true;
            }
        }

        public bool ChangeMatch(Match match)
        {
            throw new System.NotImplementedException();
        }
    }
}