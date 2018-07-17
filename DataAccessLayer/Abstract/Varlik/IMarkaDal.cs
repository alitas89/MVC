using Core.DataAccessLayer;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Varlik
{
    public interface IMarkaDal : IEntityRepository<Marka>
    {
        List<string> AddListWithTransactionBySablon(List<Marka> listMarka);
    }
}