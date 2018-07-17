using Core.DataAccessLayer;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Varlik
{
    public interface IDurusKismiDal : IEntityRepository<DurusKismi>
    {
        List<string> AddListWithTransactionBySablon(List<DurusKismi> listDurusKismi);
    }
}