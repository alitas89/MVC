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
    public class BildirimAksiyonSayfaManager : IBildirimAksiyonSayfaService
    {
        IBildirimAksiyonSayfaDal _bildirimaksiyonsayfaDal;

        public BildirimAksiyonSayfaManager(IBildirimAksiyonSayfaDal bildirimaksiyonsayfaDal)
        {
            _bildirimaksiyonsayfaDal = bildirimaksiyonsayfaDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemRead, BildirimAksiyonSayfaRead, BildirimAksiyonSayfaLtd")]
        public List<BildirimAksiyonSayfa> GetList()
        {
            return _bildirimaksiyonsayfaDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, SistemRead, BildirimAksiyonSayfaRead, BildirimAksiyonSayfaLtd")]
        public BildirimAksiyonSayfa GetById(int Id)
        {
            return _bildirimaksiyonsayfaDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemCreate, BildirimAksiyonSayfaCreate")]
        public int Add(BildirimAksiyonSayfa bildirimaksiyonsayfa)
        {
            return _bildirimaksiyonsayfaDal.Add(bildirimaksiyonsayfa);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemUpdate, BildirimAksiyonSayfaUpdate")]
        public int Update(BildirimAksiyonSayfa bildirimaksiyonsayfa)
        {
            return _bildirimaksiyonsayfaDal.Update(bildirimaksiyonsayfa);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemDelete, BildirimAksiyonSayfaDelete")]
        public int Delete(int Id)
        {
            return _bildirimaksiyonsayfaDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemDelete, BildirimAksiyonSayfaDelete")]
        public int DeleteSoft(int Id)
        {
            return _bildirimaksiyonsayfaDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, SistemRead, BildirimAksiyonSayfaRead, BildirimAksiyonSayfaLtd")]
        public List<BildirimAksiyonSayfa> GetListPagination(PagingParams pagingParams)
        {
            return _bildirimaksiyonsayfaDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _bildirimaksiyonsayfaDal.GetCount(filter);
        }

    }

}