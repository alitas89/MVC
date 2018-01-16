using System.Collections.Generic;
using BusinessLayer.Abstract;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class BilgilendirmeGrubuManager : IBilgilendirmeGrubuService
    {
        IBilgilendirmeGrubuDal _bilgilendirmegrubuDal;

        public BilgilendirmeGrubuManager(IBilgilendirmeGrubuDal bilgilendirmegrubuDal)
        {
            _bilgilendirmegrubuDal = bilgilendirmegrubuDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<BilgilendirmeGrubu> GetList()
        {
            return _bilgilendirmegrubuDal.GetList();
        }
        [SecuredOperation(Roles = "Admin,Editor")]
        public BilgilendirmeGrubu GetById(int Id)
        {
            return _bilgilendirmegrubuDal.Get(Id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(BilgilendirmeGrubu bilgilendirmegrubu)
        {
            return _bilgilendirmegrubuDal.Add(bilgilendirmegrubu);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(BilgilendirmeGrubu bilgilendirmegrubu)
        {
            return _bilgilendirmegrubuDal.Update(bilgilendirmegrubu);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _bilgilendirmegrubuDal.Delete(Id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _bilgilendirmegrubuDal.DeleteSoft(Id);
        }
    }
}