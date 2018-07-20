using Core.DataAccessLayer;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IOncelikDal : IEntityRepository<Oncelik>
    {
        List<string> AddListWithTransactionBySablon(List<Oncelik> listOncelik);
    }
}