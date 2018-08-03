using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;
using EntityLayer.ComplexTypes.DtoModel.Personel;
using System.Data;

namespace BusinessLayer.Abstract.Personel
{
    public interface IMesaiService
    {
        List<Mesai> GetList();

        Mesai GetById(int id);

        int Add(Mesai mesai);

        int Update(Mesai mesai);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Mesai> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<MesaiDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        List<string> AddListWithTransactionBySablon(List<Mesai> listMesai);

        List<Mesai> ExcelDataProcess(DataTable dataTable);
    }
}