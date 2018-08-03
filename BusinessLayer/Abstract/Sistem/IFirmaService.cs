using System.Collections.Generic;
using System.Data;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace BusinessLayer.Abstract.Sistem
{
    public interface IFirmaService
    {
        List<Firma> GetList();

        Firma GetById(int id);

        int Add(Firma firma);

        int Update(Firma firma);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Firma> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter="");

        List<Firma> ExcelDataProcess(DataTable dataTable);

        List<string> AddListWithTransactionBySablon(List<Firma> listFirma);
    }
}