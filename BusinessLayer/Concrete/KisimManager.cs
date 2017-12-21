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
    public class KisimManager : IKisimService
    {
        IKisimDal _kisimDal;

        public KisimManager(IKisimDal kisimDal)
        {
            _kisimDal = kisimDal;
        }

        public List<Kisim> GetList()
        {
            return _kisimDal.GetList();
        }
        public Kisim GetById(int Id)
        {
            return _kisimDal.Get(Id);
        }
        public int Add(Kisim kisim)
        {
            return _kisimDal.Add(kisim);
        }
        public int Update(Kisim kisim)
        {
            return _kisimDal.Update(kisim);
        }
        public int Delete(int Id)
        {
            return _kisimDal.Delete(Id);
        }
        public int DeleteSoft(int Id)
        {
            return _kisimDal.DeleteSoft(Id);
        }
    }
}
