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
    public class ArizaNedeniManager : IArizaNedeniService
    {
        IArizaNedeniDal _arizanedeniDal;

        public ArizaNedeniManager(IArizaNedeniDal arizanedeniDal)
        {
            _arizanedeniDal = arizanedeniDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimRead, ArizaNedeniRead, ArizaNedeniLtd")]
        public List<ArizaNedeni> GetList()
        {
            return _arizanedeniDal.GetList();
        }
        [SecuredOperation(Roles = "Admin, BakimRead, ArizaNedeniRead, ArizaNedeniLtd")]
        public ArizaNedeni GetById(int Id)
        {
            return _arizanedeniDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimCreate, ArizaNedeniCreate")]
        public int Add(ArizaNedeni arizanedeni)
        {
            return _arizanedeniDal.Add(arizanedeni);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimUpdate, ArizaNedeniUpdate")]
        public int Update(ArizaNedeni arizanedeni)
        {
            return _arizanedeniDal.Update(arizanedeni);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimDelete, ArizaNedeniDelete")]
        public int Delete(int Id)
        {
            return _arizanedeniDal.Delete(Id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimDelete, ArizaNedeniDelete")]
        public int DeleteSoft(int Id)
        {
            return _arizanedeniDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, ArizaNedeniRead, ArizaNedeniLtd")]
        public List<ArizaNedeni> GetListPagination(PagingParams pagingParams)
        {
            return _arizanedeniDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _arizanedeniDal.GetCount(filter);
        }
    }
}