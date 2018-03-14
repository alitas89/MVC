using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IZimmetTransferDetayService
    {
        List<ZimmetTransferDetay> GetList();

        List<ZimmetTransferDetayDto> GetList(int ZimmetTransferID);

        ZimmetTransferDetay GetById(int id);

        int Add(ZimmetTransferDetay zimmettransferdetay);

        int Update(ZimmetTransferDetay zimmettransferdetay);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<ZimmetTransferDetay> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<ZimmetTransferDetayDto> GetListPaginationDto(int ZimmetTransferID, PagingParams pagingParams);

        int GetCountDto(int ZimmetTransferID, string filter = "");
    }
}