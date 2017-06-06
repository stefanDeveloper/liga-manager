using System.Collections.Generic;
using LigaManagerServer.Models;

namespace LigaManagerServer.Interfaces
{
    public interface IBetService
    {
        List<Bet> GetBets();

        bool AddBet(Bet bet);

        bool DeleteBet(Bet bet);

        bool ChangeBet(Bet bet);
    }
}