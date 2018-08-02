using System.Collections.Generic;
using System.Data;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IArizaNedeniService
    {
        List<ArizaNedeni> GetList();

        ArizaNedeni GetById(int id);

        int Add(ArizaNedeni arizanedeni);

        int Update(ArizaNedeni arizanedeni);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<ArizaNedeni> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<string> AddListWithTransactionBySablon(List<ArizaNedeni> listArizaNedeni);

        List<ArizaNedeni> ExcelDataProcess(DataTable dataTable);
    }
}