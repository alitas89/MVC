using Core.DataAccessLayer;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Varlik
{
    public interface IHurdaDal : IEntityRepository<Hurda>
    {
        List<string> AddListWithTransactionBySablon(List<Hurda> listHurda);
    }
}