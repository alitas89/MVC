using System.Collections.Generic;
using System.Data;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace BusinessLayer.Abstract.Personel
{
    public interface IVardiyaSinifiService
    {
        List<VardiyaSinifi> GetList();

        VardiyaSinifi GetById(int id);

        int Add(VardiyaSinifi vardiyasinifi);

        int Update(VardiyaSinifi vardiyasinifi);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<VardiyaSinifi> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<string> AddListWithTransactionBySablon(List<VardiyaSinifi> listVardiyaSinifi);

        List<VardiyaSinifi> ExcelDataProcess(DataTable dataTable);
    }
}