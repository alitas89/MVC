using Core.DataAccessLayer;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IGecikmeNedeniDal : IEntityRepository<GecikmeNedeni>
    {
        List<string> AddListWithTransactionBySablon(List<GecikmeNedeni> listGecikmeNedeni);
    }
}