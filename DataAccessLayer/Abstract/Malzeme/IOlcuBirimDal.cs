using Core.DataAccessLayer;
using EntityLayer.Concrete.Malzeme;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Malzeme
{
    public interface IOlcuBirimDal : IEntityRepository<OlcuBirim>
    {
        List<string> AddListWithTransactionBySablon(List<OlcuBirim> listOlcuBirim);
    }
}