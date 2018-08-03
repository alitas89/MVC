using System.Collections.Generic;
using System.Data;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace BusinessLayer.Abstract.Malzeme
{
    public interface IMalzemeSayimiService
    {
        List<MalzemeSayimi> GetList();

        MalzemeSayimi GetById(int id);

        int Add(MalzemeSayimi malzemesayimi);

        int Update(MalzemeSayimi malzemesayimi);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<MalzemeSayimi> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter="");

        List<string> AddListWithTransactionBySablon(List<MalzemeSayimi> listMalzemeSayimi);

        List<MalzemeSayimi> ExcelDataProcess(DataTable dataTable);
    }
}