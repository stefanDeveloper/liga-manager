using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace LigaManagerServer.Framework
{
	public static class NHibernateHelper
	{
		private static ISessionFactory _sessionFactory;

		public static string DatabaseFile { get; set; }

		public static ISession OpenSession()
		{
			return SessionFactory.OpenSession();
		}

		private static ISessionFactory SessionFactory
		{
			get
			{
				if (_sessionFactory == null)
					InitializeSessionFactory();

				return _sessionFactory;
			}
		}

		private static void InitializeSessionFactory()
		{
			_sessionFactory = Fluently.Configure()
				.Database(SQLiteConfiguration.Standard.UsingFile(DatabaseFile).ShowSql())
				.Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly())
				.Conventions.Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Never()))
				.BuildSessionFactory();
		}
	}
}