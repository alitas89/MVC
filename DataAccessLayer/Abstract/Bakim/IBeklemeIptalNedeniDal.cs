using Core.DataAccessLayer;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IBeklemeIptalNedeniDal : IEntityRepository<BeklemeIptalNedeni>
    {
        List<string> AddListWithTransactionBySablon(List<BeklemeIptalNedeni> listBeklemeIptalNedeni);
    }
}