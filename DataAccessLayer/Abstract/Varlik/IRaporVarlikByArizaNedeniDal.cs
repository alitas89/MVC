using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract.Varlik
{
   public interface IRaporVarlikByArizaNedeniDal
    {
        List<VarlikDto> GetListPaginationDtoByArizaNedeniID(int ArizaNedeniID, PagingParams pagingParams);

        int GetCountDtoByArizaNedeniID(int VarlikGrupID, string filter = "");
    }
}
