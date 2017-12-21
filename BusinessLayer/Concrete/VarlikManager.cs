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
    public class VarlikDurumuManager : IVarlikDurumuService
    {
        IVarlikDurumuDal _varlikdurumuDal;

        public VarlikDurumuManager(IVarlikDurumuDal varlikdurumuDal)
        {
            _varlikdurumuDal = varlikdurumuDal;
        }

        public List<VarlikDurumu> GetList()
        {
            return _varlikdurumuDal.GetList();
        }
        public VarlikDurumu GetById(int Id)
        {
            return _varlikdurumuDal.Get(Id);
        }
        public int Add(VarlikDurumu varlikdurumu)
        {
            return _varlikdurumuDal.Add(varlikdurumu);
        }
        public int Update(VarlikDurumu varlikdurumu)
        {
            return _varlikdurumuDal.Update(varlikdurumu);
        }
        public int Delete(int Id)
        {
            return _varlikdurumuDal.Delete(Id);
        }
        public int DeleteSoft(int Id)
        {
            return _varlikdurumuDal.DeleteSoft(Id);
        }
    }
}
