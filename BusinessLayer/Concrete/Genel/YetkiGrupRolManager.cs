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
    public class YetkiGrupRolManager : IYetkiGrupRolService
    {
        IYetkiGrupRolDal _yetkigruprolDal;

        public YetkiGrupRolManager(IYetkiGrupRolDal yetkigruprolDal)
        {
            _yetkigruprolDal = yetkigruprolDal;
        }
        
        public List<YetkiGrupRol> GetList()
        {
            return _yetkigruprolDal.GetList();
        }
        
        public YetkiGrupRol GetById(int Id)
        {
            return _yetkigruprolDal.Get(Id);
        }
        
        public int Add(YetkiGrupRol yetkigruprol)
        {
            return _yetkigruprolDal.Add(yetkigruprol);
        }
        
        public int Update(YetkiGrupRol yetkigruprol)
        {
            return _yetkigruprolDal.Update(yetkigruprol);
        }
        
        public int Delete(int Id)
        {
            return _yetkigruprolDal.Delete(Id);
        }
        
        public int DeleteSoft(int Id)
        {
            return _yetkigruprolDal.DeleteSoft(Id);
        }
        
        public List<YetkiGrupRol> GetListPagination(PagingParams pagingParams)
        {
            return _yetkigruprolDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _yetkigruprolDal.GetCount(filter);
        }
        
        public List<YetkiGrupRol> GetListByGrupId(int YetkiGrupID)
        {
            return _yetkigruprolDal.GetListByGrupId(YetkiGrupID);
        }
    }
}