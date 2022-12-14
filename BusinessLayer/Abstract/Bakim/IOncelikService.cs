using System.Collections.Generic;
using System.Data;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IOncelikService
    {
        List<Oncelik> GetList();

        Oncelik GetById(int id);

        int Add(Oncelik oncelik);

        int Update(Oncelik oncelik);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Oncelik> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<string> AddListWithTransactionBySablon(List<Oncelik> listOncelik);

        List<Oncelik> ExcelDataProcess(DataTable dataTable);
    }
}