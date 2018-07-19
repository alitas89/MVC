using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IZimmetTransferService
    {
        List<ZimmetTransfer> GetList();

        ZimmetTransfer GetById(int id);

        int Add(ZimmetTransfer zimmettransfer);

        int Update(ZimmetTransfer zimmettransfer);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<ZimmetTransfer> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<ZimmetTransferDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        List<string> AddListWithTransactionBySablon(List<ZimmetTransfer> listZimmetTransfer);
  
    }
}