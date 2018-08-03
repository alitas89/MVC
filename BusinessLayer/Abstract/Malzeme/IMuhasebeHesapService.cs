using System.Collections.Generic;
using System.Data;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace BusinessLayer.Abstract.Malzeme
{
    public interface IMuhasebeHesapService
    {
        List<MuhasebeHesap> GetList();

        MuhasebeHesap GetById(int id);

        int Add(MuhasebeHesap muhasebehesap);

        int Update(MuhasebeHesap muhasebehesap);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<MuhasebeHesap> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<string> AddListWithTransactionBySablon(List<MuhasebeHesap> listMuhasebeHesap);

        List<MuhasebeHesap> ExcelDataProcess(DataTable dataTable);
    }
}