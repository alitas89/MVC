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
    public class PeriyodikBakimBildirimOkunduManager : IPeriyodikBakimBildirimOkunduService
    {
        IPeriyodikBakimBildirimOkunduDal _periyodikbakimbildirimokunduDal;

        public PeriyodikBakimBildirimOkunduManager(IPeriyodikBakimBildirimOkunduDal periyodikbakimbildirimokunduDal)
        {
            _periyodikbakimbildirimokunduDal = periyodikbakimbildirimokunduDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, PeriyodikBakimBildirimOkunduRead, PeriyodikBakimBildirimOkunduLtd")]
        public List<PeriyodikBakimBildirimOkundu> GetList()
        {
            return _periyodikbakimbildirimokunduDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, PeriyodikBakimBildirimOkunduRead, PeriyodikBakimBildirimOkunduLtd")]
        public PeriyodikBakimBildirimOkundu GetById(int Id)
        {
            return _periyodikbakimbildirimokunduDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, PeriyodikBakimBildirimOkunduCreate")]
        public int Add(PeriyodikBakimBildirimOkundu periyodikbakimbildirimokundu)
        {
            return _periyodikbakimbildirimokunduDal.Add(periyodikbakimbildirimokundu);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, PeriyodikBakimBildirimOkunduUpdate")]
        public int Update(PeriyodikBakimBildirimOkundu periyodikbakimbildirimokundu)
        {
            return _periyodikbakimbildirimokunduDal.Update(periyodikbakimbildirimokundu);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, PeriyodikBakimBildirimOkunduDelete")]
        public int Delete(int Id)
        {
            return _periyodikbakimbildirimokunduDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, PeriyodikBakimBildirimOkunduDelete")]
        public int DeleteSoft(int Id)
        {
            return _periyodikbakimbildirimokunduDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, PeriyodikBakimBildirimOkunduRead, PeriyodikBakimBildirimOkunduLtd")]
        public List<PeriyodikBakimBildirimOkundu> GetListPagination(PagingParams pagingParams)
        {
            return _periyodikbakimbildirimokunduDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _periyodikbakimbildirimokunduDal.GetCount(filter);
        }

    }

}