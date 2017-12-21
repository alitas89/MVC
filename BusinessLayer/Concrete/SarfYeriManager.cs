using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract.DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class SarfYeriManager : ISarfYeriService
    {
        ISarfYeriDal _sarfyeriDal;

        public SarfYeriManager(ISarfYeriDal sarfyeriDal)
        {
            _sarfyeriDal = sarfyeriDal;
        }

        public List<SarfYeri> GetList()
        {
            return _sarfyeriDal.GetList();
        }
        public SarfYeri GetById(int Id)
        {
            return _sarfyeriDal.Get(Id);
        }
        public int Add(SarfYeri sarfyeri)
        {
            return _sarfyeriDal.Add(sarfyeri);
        }
        public int Update(SarfYeri sarfyeri)
        {
            return _sarfyeriDal.Update(sarfyeri);
        }
        public int Delete(int Id)
        {
            return _sarfyeriDal.Delete(Id);
        }
        public int DeleteSoft(int Id)
        {
            return _sarfyeriDal.DeleteSoft(Id);
        }
    }
}
