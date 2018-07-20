using Core.DataAccessLayer;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IBakimEkibiDal : IEntityRepository<BakimEkibi>
    {
        List<string> AddListWithTransactionBySablon(List<BakimEkibi> listBakimEkibi);
    }
}