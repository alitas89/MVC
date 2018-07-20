using Core.DataAccessLayer;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IGonderimFormatiDal : IEntityRepository<GonderimFormati>
    {
        List<string> AddListWithTransactionBySablon(List<GonderimFormati> listGonderimFormati);
    }
}