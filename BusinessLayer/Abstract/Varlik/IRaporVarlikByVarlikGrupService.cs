using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IRaporVarlikByVarlikGrupService
    {
        List<VarlikDto> GetListPaginationDtoByVarlikGrupID(int VarlikGrupID, PagingParams pagingParams);

        int GetCountDtoByVarlikGrupID(int VarlikGrupID, string filter = "");
    }
}
