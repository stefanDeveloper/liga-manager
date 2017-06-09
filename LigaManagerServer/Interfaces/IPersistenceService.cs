using System.Collections.Generic;
using LigaManagerServer.Models;

namespace LigaManagerServer.Interfaces
{
    /// <summary>
    /// This services provides all functionalities to Store, Update and Get Entities of a model.
    /// </summary>
    /// <typeparam name="T">This Object extends <see cref="ModelBase"/></typeparam>
    public interface IPersistenceService <T> where T : ModelBase
    {
        T Get(int id);
        List<T> GetAll();
        bool Add(T t);
        bool Delete(T t);
        bool Update(T t);
    }
}