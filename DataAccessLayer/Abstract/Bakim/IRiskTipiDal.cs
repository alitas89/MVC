using Core.DataAccessLayer;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IRiskTipiDal : IEntityRepository<RiskTipi>
    {
        List<string> AddListWithTransactionBySablon(List<RiskTipi> listRiskTipi);
    }
}