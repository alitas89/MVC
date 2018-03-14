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
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(BilgilendirmeTuru bilgilendirmeturu)
        {
            return _bilgilendirmeturuDal.Add(bilgilendirmeturu);
        }
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(BilgilendirmeTuru bilgilendirmeturu)
        {
            return _bilgilendirmeturuDal.Update(bilgilendirmeturu);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _bilgilendirmeturuDal.Delete(Id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _bilgilendirmeturuDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<BilgilendirmeTuru> GetListPagination(PagingParams pagingParams)
        {
            return _bilgilendirmeturuDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _bilgilendirmeturuDal.GetCount(filter);
        }
    }
}