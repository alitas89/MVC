using System.Collections.Generic;
using System.Data;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace BusinessLayer.Abstract.Malzeme
{
    public interface IMalzemeAltGrupService
    {
        List<MalzemeAltGrup> GetList();

        MalzemeAltGrup GetById(int id);

        int Add(MalzemeAltGrup malzemealtgrup);

        int Update(MalzemeAltGrup malzemealtgrup);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<MalzemeAltGrup> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<string> AddListWithTransactionBySablon(List<MalzemeAltGrup> listMalzemeAltGrup);

        List<MalzemeAltGrup> ExcelDataProcess(DataTable dataTable);
    }
}