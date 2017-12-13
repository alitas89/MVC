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
    public class KullaniciRolManager : IKullaniciRolService
    {
        IKullaniciRolDal _kullanicirolDal;

        public KullaniciRolManager(IKullaniciRolDal kullanicirolDal)
        {
            _kullanicirolDal = kullanicirolDal;
        }

        public List<KullaniciRol> GetList()
        {
            return _kullanicirolDal.GetList();
        }

        public KullaniciRol GetById(int Id)
        {
            return _kullanicirolDal.Get(Id);
        }

        public int Add(KullaniciRol kullanicirol)
        {
            return _kullanicirolDal.Add(kullanicirol);
        }

        public int Update(KullaniciRol kullanicirol)
        {
            return _kullanicirolDal.Update(kullanicirol);
        }

        public int Delete(int Id)
        {
            return _kullanicirolDal.Delete(Id);
        }

        public int DeleteSoft(int Id)
        {
            return _kullanicirolDal.DeleteSoft(Id);
        }
    }
}
