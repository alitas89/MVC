using System.Collections.Generic;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class GecikmeNedeniManager : IGecikmeNedeniService
    {
        IGecikmeNedeniDal _gecikmenedeniDal;

        public GecikmeNedeniManager(IGecikmeNedeniDal gecikmenedeniDal)
        {
            _gecikmenedeniDal = gecikmenedeniDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, GecikmeNedeniRead, GecikmeNedeniLtd")]
        public List<GecikmeNedeni> GetList()
        {
            return _gecikmenedeniDal.GetList();
        }
        [SecuredOperation(Roles = "Admin, GecikmeNedeniRead, GecikmeNedeniLtd")]
        public GecikmeNedeni GetById(int Id)
        {
            return _gecikmenedeniDal.Get(Id);
        }
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, GecikmeNedeniCreate")]
        public int Add(GecikmeNedeni gecikmenedeni)
        {
            return _gecikmenedeniDal.Add(gecikmenedeni);
        }
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, GecikmeNedeniUpdate")]
        public int Update(GecikmeNedeni gecikmenedeni)
        {
            return _gecikmenedeniDal.Update(gecikmenedeni);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, GecikmeNedeniDelete")]
        public int Delete(int Id)
        {
            return _gecikmenedeniDal.Delete(Id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, GecikmeNedeniDelete")]
        public int DeleteSoft(int Id)
        {
            return _gecikmenedeniDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, GecikmeNedeniRead, GecikmeNedeniLtd")]
        public List<GecikmeNedeni> GetListPagination(PagingParams pagingParams)
        {
            return _gecikmenedeniDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _gecikmenedeniDal.GetCount(filter);
        }
    }
}