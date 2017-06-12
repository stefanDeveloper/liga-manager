using LigaManagerServer.Contracts;
using LigaManagerServer.Interfaces;
using LigaManagerServer.Models;

namespace LigaManagerServer.Services
{
    public class BettorClientService : LigaManagerService, IBettorClientService
    {
        private static readonly object StaticLock = new object();
        private readonly IPersistenceService<Bettor> _bettorPersistenceService = new PersistenceService<Bettor>();
        private readonly IPersistenceService<Bet> _betPersistenceService = new PersistenceService<Bet>();
        private readonly IPersistenceService<Season> _seasonPersistenceService = new PersistenceService<Season>();
        private readonly IPersistenceService<Match> _matchPersistenceService = new PersistenceService<Match>();

        public bool Login(string name)
        {
            lock (StaticLock)
            {
                var bettors = _bettorPersistenceService.GetAll();
                var bettor = bettors.Find(x => x.Nickname.ToUpper().Equals(name.ToUpper()));
                return bettor != null;
            }
        }

        public bool AddBet(Bet bet)
        {
            lock (StaticLock)
            {
               return _betPersistenceService.Add(bet);
            }
        }
    }
}