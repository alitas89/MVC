using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
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
    }
}
