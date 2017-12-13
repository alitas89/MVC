using Core.DataAccessLayer;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IKullaniciDal : IEntityRepository<Kullanici>
    {
        Kullanici GetByKullaniciAdiAndSifre(string kullaniciAdi, string sifre);
    }
}