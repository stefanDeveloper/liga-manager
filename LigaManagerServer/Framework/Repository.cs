using System;
using System.Collections.Generic;
using System.IO;

namespace LigaManagerServer.Framework
{
    public class Repository<T> where T: class
    {
		public Repository(string databaseFile)
		{
			NHibernateHelper.DatabaseFile = databaseFile;
		}

        public Repository()
        {
            NHibernateHelper.DatabaseFile = Path.Combine(Environment.CurrentDirectory, @"Database\", "LigaManager.db3");
        }

		public List<T> GetAll()
		{
             using (var session = NHibernateHelper.OpenSession())
             {
	             var returnList = session.CreateCriteria<T>().List<T>();
	             return returnList as List<T>;
             }
		}

		public void Delete (T entity)
		{
			using (var session = NHibernateHelper.OpenSession())
			{
				using (var transaction = session.BeginTransaction())
				{
					session.Delete(entity);
					transaction.Commit();
				}
			}
		}

		public void Save(T entity)
		{
			using (var session = NHibernateHelper.OpenSession())
			{
				using (var transaction = session.BeginTransaction())
				{
					session.Save(entity);
					transaction.Commit();
				}
			}
		}
    }
}