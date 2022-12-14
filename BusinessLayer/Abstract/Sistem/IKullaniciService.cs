using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace BusinessLayer.Abstract.Sistem
{
    public interface IKullaniciService
    {
        List<Kullanici> GetList();

        Kullanici GetById(int id);

        int Add(Kullanici kullanici);

        int Update(Kullanici kullanici);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Kullanici> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        Kullanici GetByKullaniciAdiAndSifre(string kullaniciAdi, string sifre);

        int GetKaynakIDByKullaniciID(int KullaniciID);
    }
}