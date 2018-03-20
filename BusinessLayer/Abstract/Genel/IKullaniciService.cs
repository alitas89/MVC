using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Genel;

namespace BusinessLayer.Abstract.Genel
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
    }
}