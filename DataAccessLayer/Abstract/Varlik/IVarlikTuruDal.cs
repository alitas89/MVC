using Core.DataAccessLayer;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Varlik
{
    public interface IVarlikTuruDal : IEntityRepository<VarlikTuru>
    {
        List<string> AddListWithTransactionBySablon(List<VarlikTuru> listVarlikTuru);
    }
}