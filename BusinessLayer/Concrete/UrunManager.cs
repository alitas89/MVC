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
    public class UrunManager : IUrunService
    {
        IUrunDal _urunDal;

        public UrunManager(IUrunDal urunDal)
        {
            _urunDal = urunDal;
        }

        public List<Urun> GetList()
        {
            return _urunDal.GetList();
        }
        public Urun GetById(int Id)
        {
            return _urunDal.Get(Id);
        }
        public int Add(Urun urun)
        {
            return _urunDal.Add(urun);
        }
        public int Update(Urun urun)
        {
            return _urunDal.Update(urun);
        }
        public int Delete(int Id)
        {
            return _urunDal.Delete(Id);
        }
        public int DeleteSoft(int Id)
        {
            return _urunDal.DeleteSoft(Id);
        }
    }
}
