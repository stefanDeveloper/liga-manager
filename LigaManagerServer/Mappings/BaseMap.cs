using FluentNHibernate.Mapping;
using LigaManagerServer.Models;

namespace LigaManagerServer.Mappings
{
    public class BaseMap<T> : ClassMap<T> where T : ModelBase
    {
        public BaseMap()
        {
            Id(x => x.Id).GeneratedBy.Native();
        }
    }
}