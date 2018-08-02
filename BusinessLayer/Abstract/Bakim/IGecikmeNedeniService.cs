using System.Collections.Generic;
using System.Data;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IGecikmeNedeniService
    {
        List<GecikmeNedeni> GetList();

        GecikmeNedeni GetById(int id);

        int Add(GecikmeNedeni gecikmenedeni);

        int Update(GecikmeNedeni gecikmenedeni);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<GecikmeNedeni> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<string> AddListWithTransactionBySablon(List<GecikmeNedeni> listGecikmeNedeni);

        List<GecikmeNedeni> ExcelDataProcess(DataTable dataTable);
    }
}