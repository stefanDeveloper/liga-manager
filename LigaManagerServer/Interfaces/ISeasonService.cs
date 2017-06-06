using System.Collections.Generic;
using LigaManagerServer.Models;

namespace LigaManagerServer.Interfaces
{
    public interface ISeasonService
    {
        Season GetSeason(string name);

        List<Season> GetSeasons();

        bool DeleteSeason(Season season);

        bool AddSeason(Season season);
    }
}