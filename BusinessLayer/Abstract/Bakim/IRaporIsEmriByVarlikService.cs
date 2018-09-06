using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract.Bakim
{
   public interface IRaporIsEmriByVarlikService
    {
        List<IsEmriDto> GetListPaginationDtoByVarlikID(int VarlikID, PagingParams pagingParams);

        int GetCountDtoByVarlikID(int VarlikID, string filter = "");
    }
}
