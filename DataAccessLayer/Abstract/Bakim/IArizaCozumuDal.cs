using Core.DataAccessLayer;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IArizaCozumuDal : IEntityRepository<ArizaCozumu>
    {
        List<string> AddListWithTransactionBySablon(List<ArizaCozumu> listArizaCozumu);
    }
}