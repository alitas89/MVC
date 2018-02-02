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
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(BilgilendirmeGrubu bilgilendirmegrubu)
        {
            return _bilgilendirmegrubuDal.Add(bilgilendirmegrubu);
        }
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
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

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<BilgilendirmeGrubu> GetListPagination(PagingParams pagingParams)
        {
            return _bilgilendirmegrubuDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filterCol = "", string filterVal = "")
        {
            return _bilgilendirmegrubuDal.GetCount(filterCol, filterVal);
        }
    }
}