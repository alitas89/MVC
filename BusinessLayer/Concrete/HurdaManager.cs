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
    public class HurdaManager : IHurdaService
    {
        IHurdaDal _hurdaDal;

        public HurdaManager(IHurdaDal hurdaDal)
        {
            _hurdaDal = hurdaDal;
        }

        public List<Hurda> GetList()
        {
            return _hurdaDal.GetList();
        }
        public Hurda GetById(int Id)
        {
            return _hurdaDal.Get(Id);
        }
        public int Add(Hurda hurda)
        {
            return _hurdaDal.Add(hurda);
        }
        public int Update(Hurda hurda)
        {
            return _hurdaDal.Update(hurda);
        }
        public int Delete(int Id)
        {
            return _hurdaDal.Delete(Id);
        }
        public int DeleteSoft(int Id)
        {
            return _hurdaDal.DeleteSoft(Id);
        }
    }
}
