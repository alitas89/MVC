using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace BusinessLayer.Abstract.Sistem
{
    public interface IBildirimAksiyonSayfaService
    {
        List<BildirimAksiyonSayfa> GetList();

        BildirimAksiyonSayfa GetById(int id);

        int Add(BildirimAksiyonSayfa bildirimaksiyonsayfa);

        int Update(BildirimAksiyonSayfa bildirimaksiyonsayfa);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<BildirimAksiyonSayfa> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter="");
    }
}