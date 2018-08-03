using System.Collections.Generic;
using System.Data;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace BusinessLayer.Abstract.Personel
{
    public interface IKaynakPozisyonuService
    {
        List<KaynakPozisyonu> GetList();

        KaynakPozisyonu GetById(int id);

        int Add(KaynakPozisyonu kaynakpozisyonu);

        int Update(KaynakPozisyonu kaynakpozisyonu);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<KaynakPozisyonu> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<string> AddListWithTransactionBySablon(List<KaynakPozisyonu> listKaynakPozisyonu);

        List<KaynakPozisyonu> ExcelDataProcess(DataTable dataTable);
    }
}