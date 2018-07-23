using Core.DataAccessLayer;
using EntityLayer.Concrete.Personel;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Personel
{
    public interface IKaynakSinifiDal : IEntityRepository<KaynakSinifi>
    {
        List<string> AddListWithTransactionBySablon(List<KaynakSinifi> listKaynakSinifi);
    }
}