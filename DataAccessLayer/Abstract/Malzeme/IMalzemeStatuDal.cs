using Core.DataAccessLayer;
using EntityLayer.Concrete.Malzeme;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Malzeme
{
    public interface IMalzemeStatuDal : IEntityRepository<MalzemeStatu>
    {
        List<string> AddListWithTransactionBySablon(List<MalzemeStatu> listMalzemeStatu);
    }
}