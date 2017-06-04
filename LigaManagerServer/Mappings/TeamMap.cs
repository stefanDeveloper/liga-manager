﻿using FluentNHibernate.Mapping;
using LigaManagerServer.Models;

namespace LigaManagerServer.Mappings
{
    public class TeamMap : BaseMap<Team>
    {
        public TeamMap()
        {
            Map(x => x.Name).Length(300).Not.Nullable();
        }
    }
}