using Core.DataAccessLayer;
using EntityLayer.Concrete.Malzeme;
using EntityLayer.Concrete.Personel;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Personel
{
    public interface IKaynakPozisyonuDal : IEntityRepository<KaynakPozisyonu>
    {
        List<string> AddListWithTransactionBySablon(List<KaynakPozisyonu> listKaynakPozisyonu);
    }
}