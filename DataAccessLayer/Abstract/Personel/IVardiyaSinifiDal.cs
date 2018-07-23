using Core.DataAccessLayer;
using EntityLayer.Concrete.Personel;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Personel
{
    public interface IVardiyaSinifiDal : IEntityRepository<VardiyaSinifi>
    {
        List<string> AddListWithTransactionBySablon(List<VardiyaSinifi> listVardiyaSinifi);
    }
}