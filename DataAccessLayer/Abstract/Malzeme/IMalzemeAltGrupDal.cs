using Core.DataAccessLayer;
using EntityLayer.Concrete.Malzeme;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Malzeme
{
    public interface IMalzemeAltGrupDal : IEntityRepository<MalzemeAltGrup>
    {
        List<string> AddListWithTransactionBySablon(List<MalzemeAltGrup> listMalzemeAltGrup);
    }
}