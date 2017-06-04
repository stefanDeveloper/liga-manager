using System;
using System.Collections.Generic;
using System.IO;
using LigaManagerServer.Framework;
using LigaManagerServer.Models;
using NHibernate.Mapping;

namespace LigaManagerServer.Interfaces
{
    public interface IBettorService
    {
        Bettor GetBettor(string name);
        bool AddBettor(Bettor bettor);
        bool DeleteBettor(Bettor bettor);
        List<Bettor> GetBettors();
    }
}