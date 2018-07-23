using Core.DataAccessLayer;
using EntityLayer.Concrete.Malzeme;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Malzeme
{
    public interface IMalzemeGrupDal : IEntityRepository<MalzemeGrup>
    {
        List<string> AddListWithTransactionBySablon(List<MalzemeGrup> listMalzemeGrup);
    }
}