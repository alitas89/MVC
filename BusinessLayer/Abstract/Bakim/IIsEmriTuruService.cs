using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;
using System.Data;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IIsEmriTuruService
    {
        List<IsEmriTuru> GetList();

        IsEmriTuru GetById(int id);

        int Add(IsEmriTuru ısemrituru);

        int Update(IsEmriTuru ısemrituru);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<IsEmriTuru> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<string> AddListWithTransactionBySablon(List<IsEmriTuru> listIsEmriTuru);

        List<IsEmriTuru> ExcelDataProcess(DataTable dataTable);
    }
}
