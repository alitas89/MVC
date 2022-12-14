using System.Collections.Generic;
using System.Data;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IVarlikGrupService
    {

        List<VarlikGrup> GetList();

        VarlikGrup GetById(int id);

        int Add(VarlikGrup varlikgrup);

        int Update(VarlikGrup varlikgrup);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<VarlikGrupDto> GetListDto();

        List<VarlikGrup> GetListPagination(PagingParams pagingParams);

        List<VarlikGrupDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCount(string filter = "");

        int GetCountDto(string filter = "");

        List<string> AddListWithTransactionBySablon(List<VarlikGrup> listVarlikGrup);

        List<VarlikGrup> ExcelDataProcess(DataTable dataTable);

    }
}