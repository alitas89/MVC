using Core.DataAccessLayer;
using EntityLayer.Concrete.Malzeme;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Malzeme
{
    public interface IMuhasebeHesapDal : IEntityRepository<MuhasebeHesap>
    {
        List<string> AddListWithTransactionBySablon(List<MuhasebeHesap> listMuhasebeHesap);
    }
}