using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.EntityLayer;
using DapperExtensions;

namespace Core.DataAccessLayer
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        List<T> GetList(string query, object parameters);

        List<T> GetListMapping(string query, Func<T, T, T> mapping, object parameters);

        T Get(string query, object parameters);

        int Add(string query, object parameters);

        int Update(string query, object parameters);

        int Delete(string query, object parameters);

        int DeleteSoft(string query, object parameters);
    }
}