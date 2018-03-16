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
    public class EtkiYeriManager : IEtkiYeriService
    {
        IEtkiYeriDal _etkiyeriDal;

        public EtkiYeriManager(IEtkiYeriDal etkiyeriDal)
        {
            _etkiyeriDal = etkiyeriDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, EtkiYeriRead, EtkiYeriLtd")]
        public List<EtkiYeri> GetList()
        {
            return _etkiyeriDal.GetList();
        }
        [SecuredOperation(Roles = "Admin, EtkiYeriRead, EtkiYeriLtd")]
        public EtkiYeri GetById(int Id)
        {
            return _etkiyeriDal.Get(Id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin, EtkiYeriCreate")]
        public int Add(EtkiYeri etkiyeri)
        {
            return _etkiyeriDal.Add(etkiyeri);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin, EtkiYeriUpdate")]
        public int Update(EtkiYeri etkiyeri)
        {
            return _etkiyeriDal.Update(etkiyeri);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, EtkiYeriDelete")]
        public int Delete(int Id)
        {
            return _etkiyeriDal.Delete(Id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, EtkiYeriDelete")]
        public int DeleteSoft(int Id)
        {
            return _etkiyeriDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, EtkiYeriRead, EtkiYeriLtd")]
        public List<EtkiYeri> GetListPagination(PagingParams pagingParams)
        {
            return _etkiyeriDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _etkiyeriDal.GetCount(filter);
        }
    }
}