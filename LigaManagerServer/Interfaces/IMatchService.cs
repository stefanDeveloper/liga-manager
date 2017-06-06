using System.Collections.Generic;
using LigaManagerServer.Models;

namespace LigaManagerServer.Interfaces
{
    public interface IMatchService
    {
        List<Match> GetMatches();

        bool AddMatch(Match match);

        bool ChangeMatch(Match match);
    }
}