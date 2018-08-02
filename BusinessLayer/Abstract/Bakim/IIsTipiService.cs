using System.Collections.Generic;
using System.Data;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IIsTipiService
    {
        List<IsTipi> GetList();

        IsTipi GetById(int id);

        int Add(IsTipi isTipi);

        int Update(IsTipi isTipi);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<IsTipi> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<IsTipiDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        List<string> AddListWithTransactionBySablon(List<IsTipi> listIsTipi);

        List<IsTipi> ExcelDataProcess(DataTable dataTable);


    }
}