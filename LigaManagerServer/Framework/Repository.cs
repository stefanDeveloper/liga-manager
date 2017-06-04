using System.Collections.Generic;

namespace LigaManagerServer.Framework
{
    public class Repository<T> where T: class
    {
		public Repository(string databaseFile)
		{
			NHibernateHelper.DatabaseFile = databaseFile;
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