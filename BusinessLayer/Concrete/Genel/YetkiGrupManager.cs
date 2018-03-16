using System.Collections.Generic;
using BusinessLayer.Abstract.Genel;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Genel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Genel;

namespace BusinessLayer.Concrete.Genel
{
    public class YetkiGrupManager : IYetkiGrupService
    {
        IYetkiGrupDal _yetkigrupDal;

        public YetkiGrupManager(IYetkiGrupDal yetkigrupDal)
        {
            _yetkigrupDal = yetkigrupDal;
        }
        
        public List<YetkiGrup> GetList()
        {
            return _yetkigrupDal.GetList();
        }
        
        public YetkiGrup GetById(int Id)
        {
            return _yetkigrupDal.Get(Id);
        }
        
        public int Add(YetkiGrup yetkigrup)
        {
            return _yetkigrupDal.Add(yetkigrup);
        }
        
        public int Update(YetkiGrup yetkigrup)
        {
            return _yetkigrupDal.Update(yetkigrup);
        }
        
        public int Delete(int Id)
        {
            return _yetkigrupDal.Delete(Id);
        }
        
        public int DeleteSoft(int Id)
        {
            return _yetkigrupDal.DeleteSoft(Id);
        }
        
        public List<YetkiGrup> GetListPagination(PagingParams pagingParams)
        {
            return _yetkigrupDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _yetkigrupDal.GetCount(filter);
        }

    }
}