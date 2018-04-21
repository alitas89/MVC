using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace BusinessLayer.Abstract.Sistem
{
    public interface IYetkiGrupKullaniciService
    {
        List<YetkiGrupKullanici> GetList();

        YetkiGrupKullanici GetById(int id);

        int Add(YetkiGrupKullanici yetkigrupkullanici);

        int Update(YetkiGrupKullanici yetkigrupkullanici);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<YetkiGrupKullanici> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<YetkiGrupKullanici> GetListByKullaniciId(int kullaniciId);

        int AddYetkiGrupKullanici(int kullaniciId, string arrYetkiGrup);

        int DeleteSoftByKullaniciId(int Id);

        string GetYetkiGrupListByKullaniciId(int kullaniciId);
    }
}