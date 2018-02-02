using System.Collections.Generic;
using BusinessLayer.Abstract.Genel;
using DataAccessLayer.Abstract.Genel;
using EntityLayer.Concrete.Genel;

namespace BusinessLayer.Concrete.Genel
{
    public class RolManager : IRolService
    {
        IRolDal _rolDal;

        public RolManager(IRolDal rolDal)
        {
            _rolDal = rolDal;
        }

        public List<Rol> GetList()
        {
            return _rolDal.GetList();
        }

        public Rol GetById(int Id)
        {
            return _rolDal.Get(Id);
        }

        public int Add(Rol rol)
        {
            return _rolDal.Add(rol);
        }

        public int Update(Rol rol)
        {
            return _rolDal.Update(rol);
        }

        public int Delete(int Id)
        {
            return _rolDal.Delete(Id);
        }

        public int DeleteSoft(int Id)
        {
            return _rolDal.DeleteSoft(Id);
        }

        public List<Rol> GetRolByKullaniciId(int kullaniciId)
        {
            return _rolDal.GetRolByKullaniciId(kullaniciId);
        }
    }
}
