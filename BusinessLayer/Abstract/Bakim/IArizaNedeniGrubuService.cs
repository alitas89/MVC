using System.Collections.Generic;
using System.Data;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IArizaNedeniGrubuService
    {
        List<ArizaNedeniGrubu> GetList();

        ArizaNedeniGrubu GetById(int id);

        int Add(ArizaNedeniGrubu arizanedenigrubu);

        int Update(ArizaNedeniGrubu arizanedenigrubu);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<ArizaNedeniGrubu> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<string> AddListWithTransactionBySablon(List<ArizaNedeniGrubu> listArizaNedeniGrubu);

        List<ArizaNedeniGrubu> ExcelDataProcess(DataTable dataTable);
    }
}