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
    public class DurusKismiManager : IDurusKismiService
    {
        IDurusKismiDal _duruskismiDal;

        public DurusKismiManager(IDurusKismiDal duruskismiDal)
        {
            _duruskismiDal = duruskismiDal;
        }

        public List<DurusKismi> GetList()
        {
            return _duruskismiDal.GetList();
        }
        public DurusKismi GetById(int Id)
        {
            return _duruskismiDal.Get(Id);
        }
        public int Add(DurusKismi duruskismi)
        {
            return _duruskismiDal.Add(duruskismi);
        }
        public int Update(DurusKismi duruskismi)
        {
            return _duruskismiDal.Update(duruskismi);
        }
        public int Delete(int Id)
        {
            return _duruskismiDal.Delete(Id);
        }
        public int DeleteSoft(int Id)
        {
            return _duruskismiDal.DeleteSoft(Id);
        }
    }
}
