using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract.Iot
{
    public interface ISayacService
    {
        List<Sayac> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<Sayac> GetListByModemSeriNo(string modemSeriNo);
    }
}
