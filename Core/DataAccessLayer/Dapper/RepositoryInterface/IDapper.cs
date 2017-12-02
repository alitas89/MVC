using System.Collections.Generic;
using Core.EntityLayer;

namespace Core.DataAccessLayer.Dapper.RepositoryInterface
{
    public interface IDapper<T> where T : class, IEntity, new()
    {
        List<T> GetListQuery(string query, object parameters);

        T GetQuery(string query, object parameters);

        int AddQuery(string query, object parameters);

        int UpdateQuery(string query, object parameters);

        int DeleteQuery(string query, object parameters);

        int DeleteSoftQuery(string query, object parameters);
    }
}