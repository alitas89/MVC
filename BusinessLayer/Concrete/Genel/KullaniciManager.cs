using System.Collections.Generic;
using BusinessLayer.Abstract.Genel;
using DataAccessLayer.Abstract.Genel;
using EntityLayer.Concrete.Genel;

namespace BusinessLayer.Concrete.Genel
{
    public class KullaniciManager : IKullaniciService
    {
        IKullaniciDal _kullaniciDal;

        public KullaniciManager(IKullaniciDal kullaniciDal)
        {
            _kullaniciDal = kullaniciDal;
        }

        public List<Kullanici> GetList()
        {
            return _kullaniciDal.GetList();
        }

        public Kullanici GetById(int Id)
        {
            return _kullaniciDal.Get(Id);
        }

        public int Add(Kullanici kullanici)
        {
            return _kullaniciDal.Add(kullanici);
        }

        public int Update(Kullanici kullanici)
        {
            return _kullaniciDal.Update(kullanici);
        }

        public int Delete(int Id)
        {
            return _kullaniciDal.Delete(Id);
        }

        public int DeleteSoft(int Id)
        {
            return _kullaniciDal.DeleteSoft(Id);
        }

        public Kullanici GetByKullaniciAdiAndSifre(string kullaniciAdi, string sifre)
        {
            return _kullaniciDal.GetByKullaniciAdiAndSifre(kullaniciAdi, sifre);
        }
    }
}
