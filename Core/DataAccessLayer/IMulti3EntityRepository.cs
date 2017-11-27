using System.Collections.Generic;
using Core.EntityLayer;

namespace Core.DataAccessLayer
{
    public interface IMulti3EntityRepository<TA, TB, TC>
        where TA : class, IEntity, new()
        where TB : class, IEntity, new()
        where TC : class, IEntity, new()
    {

        //IQueryable<TC> GetListMapping(string query, Func<TA, TB, TC> mapping, object parameters);

        List<TA> GetListMapping(string query, string splitOn);
    }
}