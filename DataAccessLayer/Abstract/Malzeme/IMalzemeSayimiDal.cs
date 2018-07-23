using Core.DataAccessLayer;
using EntityLayer.Concrete.Malzeme;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Malzeme
{
    public interface IMalzemeSayimiDal : IEntityRepository<MalzemeSayimi>
    {
        List<string> AddListWithTransactionBySablon(List<MalzemeSayimi> listMalzemeSayimi);
    }
}