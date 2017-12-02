using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.EntityLayer;
using DapperExtensions;

namespace Core.DataAccessLayer
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        List<T> GetList();

        T Get(int Id);

        int Add(T obj);

        int Update(T obj);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}