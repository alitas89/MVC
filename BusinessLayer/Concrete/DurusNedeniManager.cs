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
    public class DurusNedeniManager : IDurusNedeniService
    {
        IDurusNedeniDal _durusnedeniDal;

        public DurusNedeniManager(IDurusNedeniDal durusnedeniDal)
        {
            _durusnedeniDal = durusnedeniDal;
        }

        public List<DurusNedeni> GetList()
        {
            return _durusnedeniDal.GetList();
        }
        public DurusNedeni GetById(int Id)
        {
            return _durusnedeniDal.Get(Id);
        }
        public int Add(DurusNedeni durusnedeni)
        {
            return _durusnedeniDal.Add(durusnedeni);
        }
        public int Update(DurusNedeni durusnedeni)
        {
            return _durusnedeniDal.Update(durusnedeni);
        }
        public int Delete(int Id)
        {
            return _durusnedeniDal.Delete(Id);
        }
        public int DeleteSoft(int Id)
        {
            return _durusnedeniDal.DeleteSoft(Id);
        }
    }
}
