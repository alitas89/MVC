using System.Collections.Generic;
using System.Data;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.ComplexTypes.DtoModel.Others;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IKisimService
    {
        List<Kisim> GetList();

        List<Kisim> GetList(int SarfYeriID);

        Kisim GetById(int id);

        int Add(Kisim kisim);

        int Update(Kisim kisim);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<KisimDto> GetListDto();

        List<Kisim> GetListPagination(PagingParams pagingParams);

        List<KisimDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCount(string filter = "");

        int GetCountDto(string filter = "");

        List<string> AddListWithTransactionBySablon(List<Kisim> listKisim);

        List<ColumnNameTemp> GetColumnNames(string tableName);

        List<Kisim> ExcelDataProcess(DataTable dataTable);
    }
}