using Core.DataAccessLayer;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IHizmetDal : IEntityRepository<Hizmet>
    {
        List<string> AddListWithTransactionBySablon(List<Hizmet> listHizmet);
    }
}