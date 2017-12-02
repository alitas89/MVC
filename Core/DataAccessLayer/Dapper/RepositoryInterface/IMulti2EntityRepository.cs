using System.Collections.Generic;
using Core.EntityLayer;

namespace Core.DataAccessLayer.Dapper.RepositoryInterface
{
    public interface IMulti2EntityRepository<TA, TB> 
        where TA : class, IEntity, new()
        where TB : class, IEntity, new()
    {

        //IQueryable<TC> GetListMapping(string query, Func<TA, TB, TC> mapping, object parameters);

        List<TA> GetListMappingQuery(string query, string splitOn);
    }
}