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
    public class VarlikGrupManager : IVarlikGrupService
    {
        IVarlikGrupDal _varlikgrupDal;

        public VarlikGrupManager(IVarlikGrupDal varlikgrupDal)
        {
            _varlikgrupDal = varlikgrupDal;
        }

        public List<VarlikGrup> GetList()
        {
            return _varlikgrupDal.GetList();
        }
        public VarlikGrup GetById(int Id)
        {
            return _varlikgrupDal.Get(Id);
        }
        public int Add(VarlikGrup varlikgrup)
        {
            return _varlikgrupDal.Add(varlikgrup);
        }
        public int Update(VarlikGrup varlikgrup)
        {
            return _varlikgrupDal.Update(varlikgrup);
        }
        public int Delete(int Id)
        {
            return _varlikgrupDal.Delete(Id);
        }
        public int DeleteSoft(int Id)
        {
            return _varlikgrupDal.DeleteSoft(Id);
        }
    }
}
