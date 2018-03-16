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
    public class YetkiRolManager : IYetkiRolService
    {
        IYetkiRolDal _yetkirolDal;

        public YetkiRolManager(IYetkiRolDal yetkirolDal)
        {
            _yetkirolDal = yetkirolDal;
        }
        
        public List<YetkiRol> GetList()
        {
            return _yetkirolDal.GetList();
        }
        
        public YetkiRol GetById(int Id)
        {
            return _yetkirolDal.Get(Id);
        }
        
        public int Add(YetkiRol yetkirol)
        {
            return _yetkirolDal.Add(yetkirol);
        }
        
        public int Update(YetkiRol yetkirol)
        {
            return _yetkirolDal.Update(yetkirol);
        }
        
        public int Delete(int Id)
        {
            return _yetkirolDal.Delete(Id);
        }
        
        public int DeleteSoft(int Id)
        {
            return _yetkirolDal.DeleteSoft(Id);
        }
        
        public List<YetkiRol> GetListPagination(PagingParams pagingParams)
        {
            return _yetkirolDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _yetkirolDal.GetCount(filter);
        }

    }
}