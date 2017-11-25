using System;
using System.Collections.Generic;
using Core.EntityLayer;

namespace Core.DataAccessLayer
{
    public interface IEntityMultiRepository<TA, out TB, in TC>
        where TA : class, IEntity, new()
        where TB : class, IEntity, new()
        where TC : class, IEntity, new()
    {
        List<TA> GetListMapping2(string query, Func<TA, TB, TC> mapping, object parameters);
    }
}