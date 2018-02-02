using Core.DataAccessLayer;
using EntityLayer.Concrete.Genel;

namespace DataAccessLayer.Abstract.Genel
{
    public interface IKullaniciDal : IEntityRepository<Kullanici>
    {
        Kullanici GetByKullaniciAdiAndSifre(string kullaniciAdi, string sifre);
    }
}