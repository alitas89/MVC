using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IBakimEkibiUyeDal : IEntityRepository<BakimEkibiUye>
    {
        int AddWithTransaction(int BakimEkibiID, List<int> listKaynakID);

        List<BakimEkibiUye> GetListByBakimEkibiID(int BakimEkibiID);
    }
}