using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract.Bakim
{
     public interface IRaporArizaNedeniDal
    {
        List<ArizaNedeni> GetListPaginationDto( PagingParams pagingParams);

        int GetCountDto(string filter = "");
    }
}
