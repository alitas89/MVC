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
    public class IsletmeManager : IIsletmeService
    {
        IIsletmeDal _ısletmeDal;

        public IsletmeManager(IIsletmeDal ısletmeDal)
        {
            _ısletmeDal = ısletmeDal;
        }

        public List<Isletme> GetList()
        {
            return _ısletmeDal.GetList();
        }
        public Isletme GetById(int Id)
        {
            return _ısletmeDal.Get(Id);
        }
        public int Add(Isletme ısletme)
        {
            return _ısletmeDal.Add(ısletme);
        }
        public int Update(Isletme ısletme)
        {
            return _ısletmeDal.Update(ısletme);
        }
        public int Delete(int Id)
        {
            return _ısletmeDal.Delete(Id);
        }
        public int DeleteSoft(int Id)
        {
            return _ısletmeDal.DeleteSoft(Id);
        }
    }
}
