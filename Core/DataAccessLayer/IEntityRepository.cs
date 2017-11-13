using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.EntityLayer;
using DapperExtensions;

namespace Core.DataAccessLayer
{
    public interface IEntityRepository<T> where T: class, IEntity, new()
    {
        List<T> GetList(Expression<Func<T,bool>> filter=null);

        T Get(IFieldPredicate filter = null);

        T Add(T entity);

        T Update(T entity);

        void Delete(T entity);
    }
}