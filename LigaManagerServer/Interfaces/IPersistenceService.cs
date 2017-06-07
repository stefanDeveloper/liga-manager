using System.Collections.Generic;
using LigaManagerServer.Models;

namespace LigaManagerServer.Interfaces
{
    public interface IPersistenceService <T> where T : ModelBase
    {
        T Get(int id);
        List<T> GetAll();
        bool Add(T t);
        bool Delete(T t);
        bool Change(T t);
    }
}