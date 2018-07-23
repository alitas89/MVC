using Core.DataAccessLayer;
using EntityLayer.Concrete.Personel;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Personel
{
    public interface IEgitimDal : IEntityRepository<Egitim>
    {
        List<string> AddListWithTransactionBySablon(List<Egitim> listEgitim);
    }
}