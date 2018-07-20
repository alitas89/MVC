using Core.DataAccessLayer;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IBakimOncelikDal : IEntityRepository<BakimOncelik>
    {
        List<string> AddListWithTransactionBySablon(List<BakimOncelik> listBakimOncelik);
    }
}