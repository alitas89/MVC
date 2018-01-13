using System.Collections.Generic;

namespace Core.DataAccessLayer.Dapper.RepositoryInterface
{
    public interface IDto<T>
    {
        List<T> GetListDtoQuery(string query, object parameters);
    }
}