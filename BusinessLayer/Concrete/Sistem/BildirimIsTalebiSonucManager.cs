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
    public class BildirimIsTalebiSonucManager : IBildirimIsTalebiSonucService
    {
        IBildirimIsTalebiSonucDal _bildirimıstalebisonucDal;

        public BildirimIsTalebiSonucManager(IBildirimIsTalebiSonucDal bildirimıstalebisonucDal)
        {
            _bildirimıstalebisonucDal = bildirimıstalebisonucDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemRead, IsTalebiRead")]
        public List<BildirimIsTalebiSonuc> GetList()
        {
            return _bildirimıstalebisonucDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, SistemRead, IsTalebiRead")]
        public BildirimIsTalebiSonuc GetById(int Id)
        {
            return _bildirimıstalebisonucDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemRead, IsTalebiRead")]
        public int Add(BildirimIsTalebiSonuc bildirimıstalebisonuc)
        {
            return _bildirimıstalebisonucDal.Add(bildirimıstalebisonuc);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemRead, IsTalebiRead")]
        public int Update(BildirimIsTalebiSonuc bildirimıstalebisonuc)
        {
            return _bildirimıstalebisonucDal.Update(bildirimıstalebisonuc);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemRead, IsTalebiRead")]
        public int Delete(int Id)
        {
            return _bildirimıstalebisonucDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemRead, IsTalebiRead")]
        public int DeleteSoft(int Id)
        {
            return _bildirimıstalebisonucDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, SistemRead, IsTalebiRead")]
        public List<BildirimIsTalebiSonuc> GetListPagination(PagingParams pagingParams)
        {
            return _bildirimıstalebisonucDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _bildirimıstalebisonucDal.GetCount(filter);
        }

    }
}