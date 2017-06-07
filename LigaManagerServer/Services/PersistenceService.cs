using System.Collections.Generic;
using LigaManagerServer.Framework;
using LigaManagerServer.Interfaces;
using LigaManagerServer.Models;
using NHibernate.SqlCommand;

namespace LigaManagerServer.Services
{
    public class PersistenceService<T> : IPersistenceService<T>  where T : ModelBase 
    {
        private readonly Repository<T> _repository = new Repository<T>();

        public T Get(int id)
        {
            var all = _repository.GetAll();
            return all.Find(x => x.Id.Equals(id));
        }

        public bool Add(T t)
        {
            var all = _repository.GetAll();
            var find = all.Find(x => x.Equals(t));
            if (find != null) return false;
            _repository.Save(t);
            return true;
        }

        public bool Delete(T t)
        {
            var all = _repository.GetAll();
            var find = all.Find(x => x.Equals(t));
            if (find == null) return false;
            _repository.Delete(find);
            return true;
        }

        public bool Change(T t)
        {
            var all = _repository.GetAll();
            var find = all.Find(x => x.Id.Equals(t.Id));
            if (find == null) return false;
            _repository.Save(t);
            return true;
        }

        public List<T> GetAll()
        {
            var t = _repository.GetAll();
            return t;
        }
    }
}