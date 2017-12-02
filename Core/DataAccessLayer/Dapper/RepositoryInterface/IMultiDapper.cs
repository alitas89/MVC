using System.Collections.Generic;
using System.Runtime.Hosting;
using Core.EntityLayer;

namespace Core.DataAccessLayer.Dapper.RepositoryInterface
{
    public interface IMultiDapper<T> where T : class, IEntity, new()
    {

    }
}