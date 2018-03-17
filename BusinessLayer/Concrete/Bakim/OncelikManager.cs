using System.Collections.Generic;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class OncelikManager : IOncelikService
    {
        IOncelikDal _oncelikDal;

        public OncelikManager(IOncelikDal oncelikDal)
        {
            _oncelikDal = oncelikDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimRead, OncelikRead, OncelikLtd")]
        public List<Oncelik> GetList()
        {
            return _oncelikDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, BakimRead, OncelikRead, OncelikLtd")]
        public Oncelik GetById(int Id)
        {
            return _oncelikDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimCreate, OncelikCreate")]
        public int Add(Oncelik oncelik)
        {
            return _oncelikDal.Add(oncelik);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimUpdate, OncelikUpdate")]
        public int Update(Oncelik oncelik)
        {
            return _oncelikDal.Update(oncelik);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimDelete, OncelikDelete")]
        public int Delete(int Id)
        {
            return _oncelikDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimDelete, OncelikDelete")]
        public int DeleteSoft(int Id)
        {
            return _oncelikDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, OncelikRead, OncelikLtd")]
        public List<Oncelik> GetListPagination(PagingParams pagingParams)
        {
            return _oncelikDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _oncelikDal.GetCount(filter);
        }
    }
}