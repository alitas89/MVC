using System;
using System.Collections.Generic;
using System.Linq;
using Core.EntityLayer;

namespace Core.DataAccessLayer
{
    public interface IMultiEntityRepository<TA, TB, TC> 
        where TA : class, IEntity, new()
        where TB : class, IEntity, new()
        where TC : class, IEntity, new()
    {

        IQueryable<TC> GetListMapping(string query, Func<TA, TB, TC> mapping, object parameters);

    }
}