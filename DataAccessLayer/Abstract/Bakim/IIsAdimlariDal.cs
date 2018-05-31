using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IIsAdimlariDal : IEntityRepository<IsAdimlari>
    {

        List<IsAdimlari> GetListIsAdimlariByBakimPlaniID(int BakimPlaniID);
    }
}