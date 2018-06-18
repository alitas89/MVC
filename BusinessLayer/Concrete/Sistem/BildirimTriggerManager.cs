using System.Collections.Generic;
using BusinessLayer.Abstract.Sistem;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace BusinessLayer.Concrete.Sistem
{
    public class BildirimTriggerManager : IBildirimTriggerService
    {
        IBildirimTriggerDal _bildirimtriggerDal;

        public BildirimTriggerManager(IBildirimTriggerDal bildirimtriggerDal)
        {
            _bildirimtriggerDal = bildirimtriggerDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemRead, BildirimTriggerRead, BildirimTriggerLtd")]
        public List<BildirimTrigger> GetList()
        {
            return _bildirimtriggerDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, SistemRead, BildirimTriggerRead, BildirimTriggerLtd")]
        public BildirimTrigger GetById(int Id)
        {
            return _bildirimtriggerDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemCreate, BildirimTriggerCreate")]
        public int Add(BildirimTrigger bildirimtrigger)
        {
            return _bildirimtriggerDal.Add(bildirimtrigger);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemUpdate, BildirimTriggerUpdate")]
        public int Update(BildirimTrigger bildirimtrigger)
        {
            return _bildirimtriggerDal.Update(bildirimtrigger);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemDelete, BildirimTriggerDelete")]
        public int Delete(int Id)
        {
            return _bildirimtriggerDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemDelete, BildirimTriggerDelete")]
        public int DeleteSoft(int Id)
        {
            return _bildirimtriggerDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, SistemRead, BildirimTriggerRead, BildirimTriggerLtd")]
        public List<BildirimTrigger> GetListPagination(PagingParams pagingParams)
        {
            return _bildirimtriggerDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _bildirimtriggerDal.GetCount(filter);
        }

    }

}