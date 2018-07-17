using Core.DataAccessLayer;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Varlik
{
    public interface IVarlikDurumuDal : IEntityRepository<VarlikDurumu>
    {
        List<string> AddListWithTransactionBySablon(List<VarlikDurumu> listVarlikDurumu);

    }
}