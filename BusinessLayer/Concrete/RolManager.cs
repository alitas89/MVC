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
    }
}
