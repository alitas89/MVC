using Core.DataAccessLayer;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Varlik
{
    public interface IUrunDal : IEntityRepository<Urun>
    {
        List<string> AddListWithTransactionBySablon(List<Urun> listUrun);

    }
}