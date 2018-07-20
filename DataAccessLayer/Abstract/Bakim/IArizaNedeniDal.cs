using Core.DataAccessLayer;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IArizaNedeniDal : IEntityRepository<ArizaNedeni>
    {
        List<string> AddListWithTransactionBySablon(List<ArizaNedeni> listArizaNedeni);
    }
}