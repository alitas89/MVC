using System.Collections.Generic;
using System.Data;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IStatuService
    {
        List<Statu> GetList();

        Statu GetById(int id);

        int Add(Statu statu);

        int Update(Statu statu);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Statu> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<StatuDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        List<string> AddListWithTransactionBySablon(List<Statu> listStatu);

        List<Statu> ExcelDataProcess(DataTable dataTable);
    }
}