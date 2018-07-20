using Core.DataAccessLayer;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IIsEmriTuruDal : IEntityRepository<IsEmriTuru>
    {
        List<string> AddListWithTransactionBySablon(List<IsEmriTuru> listIsEmriTuru);
    }
}
