using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Varlik;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IAracServisService
    {
        List<AracServis> GetList();

        AracServis GetById(int id);

        int Add(AracServis aracservis);

        int Update(AracServis aracservis);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<AracServis> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<AracServisDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        List<string> AddListWithTransactionBySablon(List<AracServis> listAracServis);

    }
}
