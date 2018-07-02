using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Iot
{
    public interface ISayacDal : IEntityRepository<Sayac>
    {
        List<Sayac> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<Sayac> GetListByModemSeriNo(string ModemSeriNo);
    }
}
