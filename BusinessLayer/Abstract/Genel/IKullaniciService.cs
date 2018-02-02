using EntityLayer.Concrete.Genel;

namespace BusinessLayer.Abstract.Genel
{
    public interface IKullaniciService
    {
        Kullanici GetById(int id);

        int Add(Kullanici kullanici);

        int Update(Kullanici kullanici);

        int Delete(int Id);

        int DeleteSoft(int Id);

        Kullanici GetByKullaniciAdiAndSifre(string kullaniciAdi, string sifre);
    }
}