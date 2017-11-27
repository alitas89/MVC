using System;
using System.Collections.Generic;
using System.Linq;
using Core.EntityLayer;

namespace Core.DataAccessLayer
{
    public interface IMulti2EntityRepository<TA, TB> 
        where TA : class, IEntity, new()
        where TB : class, IEntity, new()
    {

        //IQueryable<TC> GetListMapping(string query, Func<TA, TB, TC> mapping, object parameters);

        List<TA> GetListMapping(string query, string splitOn);
    }
}