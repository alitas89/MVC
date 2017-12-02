using System.Collections.Generic;
using Core.EntityLayer;

namespace Core.DataAccessLayer.Dapper.RepositoryInterface
{
    public interface IMulti3EntityRepository<TA, TB, TC>
        where TA : class, IEntity, new()
        where TB : class, IEntity, new()
        where TC : class, IEntity, new()
    {

        //IQueryable<TC> GetListMapping(string query, Func<TA, TB, TC> mapping, object parameters);

        List<TA> GetListMappingQuery(string query, string splitOn);
    }
}