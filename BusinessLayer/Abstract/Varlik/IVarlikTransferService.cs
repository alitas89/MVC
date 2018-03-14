using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IVarlikTransferService
    {
        List<VarlikTransfer> GetList();

        VarlikTransfer GetById(int id);

        int Add(VarlikTransfer varliktransfer);

        int Update(VarlikTransfer varliktransfer);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<VarlikTransfer> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<VarlikTransferDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");
    }
}