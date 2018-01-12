using System.Collections.Generic;
using BusinessLayer.Abstract;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class BilgilendirmeTuruManager : IBilgilendirmeTuruService
    {
        IBilgilendirmeTuruDal _bilgilendirmeturuDal;

        public BilgilendirmeTuruManager(IBilgilendirmeTuruDal bilgilendirmeturuDal)
        {
            _bilgilendirmeturuDal = bilgilendirmeturuDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<BilgilendirmeTuru> GetList()
        {
            return _bilgilendirmeturuDal.GetList();
        }
        [SecuredOperation(Roles = "Admin,Editor")]
        public BilgilendirmeTuru GetById(int Id)
        {
            return _bilgilendirmeturuDal.Get(Id);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(BilgilendirmeTuru bilgilendirmeturu)
        {
            return _bilgilendirmeturuDal.Add(bilgilendirmeturu);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(BilgilendirmeTuru bilgilendirmeturu)
        {
            return _bilgilendirmeturuDal.Update(bilgilendirmeturu);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _bilgilendirmeturuDal.Delete(Id);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _bilgilendirmeturuDal.DeleteSoft(Id);
        }
    }
}