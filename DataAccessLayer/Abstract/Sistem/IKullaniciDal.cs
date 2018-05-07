using Core.DataAccessLayer;
using EntityLayer.Concrete.Sistem;

namespace DataAccessLayer.Abstract.Sistem
{
    public interface IKullaniciDal : IEntityRepository<Kullanici>
    {
        Kullanici GetByKullaniciAdiAndSifre(string kullaniciAdi, string sifre);

        int GetKaynakIDByKullaniciID(int KullaniciID);
    }
}