using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IRaporVarlikByArizaNedeniService
    {
        List<VarlikDto> GetListPaginationDtoByArizaNedeniID(int ArizaNedeniID, PagingParams pagingParams);

        int GetCountDtoByArizaNedeniID(int ArizaNedeniID, string filter = "");

    }
}
