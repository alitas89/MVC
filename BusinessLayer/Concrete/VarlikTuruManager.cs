using System.Collections.Generic;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class VarlikTuruManager : IVarlikTuruService
    {
        IVarlikTuruDal _varlikturuDal;

        public VarlikTuruManager(IVarlikTuruDal varlikturuDal)
        {
            _varlikturuDal = varlikturuDal;
        }

        public List<VarlikTuru> GetList()
        {
            return _varlikturuDal.GetList();
        }
        public VarlikTuru GetById(int Id)
        {
            return _varlikturuDal.Get(Id);
        }
        public int Add(VarlikTuru varlikturu)
        {
            return _varlikturuDal.Add(varlikturu);
        }
        public int Update(VarlikTuru varlikturu)
        {
            return _varlikturuDal.Update(varlikturu);
        }
        public int Delete(int Id)
        {
            return _varlikturuDal.Delete(Id);
        }
        public int DeleteSoft(int Id)
        {
            return _varlikturuDal.DeleteSoft(Id);
        }
    }
}