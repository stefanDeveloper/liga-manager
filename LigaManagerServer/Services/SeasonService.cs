using System;
using System.Collections.Generic;
using System.IO;
using LigaManagerServer.Framework;
using LigaManagerServer.Interfaces;
using LigaManagerServer.Models;

namespace LigaManagerServer.Services
{
    public class SeasonService : ISeasonService
    {
        private readonly Repository<Season> _repository = new Repository<Season>();
        private static readonly object StaticLock = new object();

        public Season GetSeason(string name)
        {
            lock (StaticLock)
            {
                var seasons = _repository.GetAll();
                var season = seasons.Find(x => x.Name.Equals(name));
                return season;
            }
        }

        public List<Season> GetSeasons()
        {
            lock (StaticLock)
            {
                var seasons = _repository.GetAll();
                return seasons;
            }
        }

        public bool DeleteSeason(Season season)
        {
            lock (StaticLock)
            {
                var seasons = _repository.GetAll();
                var find = seasons.Find(x => x.Name.Equals(season.Name));
                if (find == null) return false;
                _repository.Delete(find);
                return true;
            }
        }

        public bool AddSeason(Season season)
        {
            lock (StaticLock)
            {
                var seasons = _repository.GetAll();
                var find = seasons.Find(x => x.Name.ToUpper().Equals(season.Name.ToUpper()));
                if (find != null) return false;
                _repository.Save(season);
                return true;
            }
        }
    }
}