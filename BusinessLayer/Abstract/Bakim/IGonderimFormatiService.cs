using System.Collections.Generic;
using System.Data;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IGonderimFormatiService
    {
        List<GonderimFormati> GetList();

        GonderimFormati GetById(int id);

        int Add(GonderimFormati gonderimformati);

        int Update(GonderimFormati gonderimformati);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<GonderimFormati> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<string> AddListWithTransactionBySablon(List<GonderimFormati> listGonderimFormati);

        List<GonderimFormati> ExcelDataProcess(DataTable dataTable);
    }
}