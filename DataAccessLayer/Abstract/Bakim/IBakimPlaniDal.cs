using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IBakimPlaniDal : IEntityRepository<BakimPlani>
    {
        int AddWithTransaction(BakimPlani bakimplani, List<IsAdimlari> listIsAdimlari);

        int UpdateWithTransaction(BakimPlani bakimplani, List<IsAdimlari> listIsAdimlari);
    }
}