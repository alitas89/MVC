using System.Collections.Generic;
using BusinessLayer.Abstract;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class EtkiYeriManager : IEtkiYeriService
    {
        IEtkiYeriDal _etkiyeriDal;

        public EtkiYeriManager(IEtkiYeriDal etkiyeriDal)
        {
            _etkiyeriDal = etkiyeriDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<EtkiYeri> GetList()
        {
            return _etkiyeriDal.GetList();
        }
        [SecuredOperation(Roles = "Admin,Editor")]
        public EtkiYeri GetById(int Id)
        {
            return _etkiyeriDal.Get(Id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(EtkiYeri etkiyeri)
        {
            return _etkiyeriDal.Add(etkiyeri);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(EtkiYeri etkiyeri)
        {
            return _etkiyeriDal.Update(etkiyeri);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _etkiyeriDal.Delete(Id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _etkiyeriDal.DeleteSoft(Id);
        }
    }
}