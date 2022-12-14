using System.Collections.Generic;
using System.Data;
using EntityLayer.ComplexTypes.DtoModel.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace BusinessLayer.Abstract.Personel
{
    public interface IVardiyaService
    {
        List<Vardiya> GetList();

        Vardiya GetById(int id);

        int Add(Vardiya vardiya);

        int Update(Vardiya vardiya);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Vardiya> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<VardiyaDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        List<string> AddListWithTransactionBySablon(List<Vardiya> listVardiya);

        List<Vardiya> ExcelDataProcess(DataTable dataTable);
    }
}