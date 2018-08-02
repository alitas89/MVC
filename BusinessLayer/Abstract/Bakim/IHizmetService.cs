using System.Collections.Generic;
using System.Data;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IHizmetService
    {
        List<Hizmet> GetList();

        Hizmet GetById(int id);

        int Add(Hizmet hizmet);

        int Update(Hizmet hizmet);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Hizmet> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<string> AddListWithTransactionBySablon(List<Hizmet> listHizmet);

        List<Hizmet> ExcelDataProcess(DataTable dataTable);


    }
}