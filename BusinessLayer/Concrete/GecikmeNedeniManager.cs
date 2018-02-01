using System.Collections.Generic;
using BusinessLayer.Abstract;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class GecikmeNedeniManager : IGecikmeNedeniService
    {
        IGecikmeNedeniDal _gecikmenedeniDal;

        public GecikmeNedeniManager(IGecikmeNedeniDal gecikmenedeniDal)
        {
            _gecikmenedeniDal = gecikmenedeniDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<GecikmeNedeni> GetList()
        {
            return _gecikmenedeniDal.GetList();
        }
        [SecuredOperation(Roles = "Admin,Editor")]
        public GecikmeNedeni GetById(int Id)
        {
            return _gecikmenedeniDal.Get(Id);
        }
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(GecikmeNedeni gecikmenedeni)
        {
            return _gecikmenedeniDal.Add(gecikmenedeni);
        }
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(GecikmeNedeni gecikmenedeni)
        {
            return _gecikmenedeniDal.Update(gecikmenedeni);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _gecikmenedeniDal.Delete(Id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _gecikmenedeniDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<GecikmeNedeni> GetListPagination(PagingParams pagingParams)
        {
            return _gecikmenedeniDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filterCol = "", string filterVal = "")
        {
            return _gecikmenedeniDal.GetCount(filterCol, filterVal);
        }
    }
}