using System.Collections.Generic;
using System.Data;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IVarlikDurumuService
    {
        List<VarlikDurumu> GetList();

        VarlikDurumu GetById(int id);

        int Add(VarlikDurumu varlikdurumu);

        int Update(VarlikDurumu varlikdurumu);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<VarlikDurumu> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<string> AddListWithTransactionBySablon(List<VarlikDurumu> listVarlikDurumu);

        List<VarlikDurumu> ExcelDataProcess(DataTable dataTable);

    }
}