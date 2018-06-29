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
    public class PeriyodikBakimBildirimPushedManager : IPeriyodikBakimBildirimPushedService
    {
        IPeriyodikBakimBildirimPushedDal _periyodikbakimbildirimpushedDal;

        public PeriyodikBakimBildirimPushedManager(IPeriyodikBakimBildirimPushedDal periyodikbakimbildirimpushedDal)
        {
            _periyodikbakimbildirimpushedDal = periyodikbakimbildirimpushedDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, PeriyodikBakimBildirimPushedRead, PeriyodikBakimBildirimPushedLtd")]
        public List<PeriyodikBakimBildirimPushed> GetList()
        {
            return _periyodikbakimbildirimpushedDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, PeriyodikBakimBildirimPushedRead, PeriyodikBakimBildirimPushedLtd")]
        public PeriyodikBakimBildirimPushed GetById(int Id)
        {
            return _periyodikbakimbildirimpushedDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Authorized")]
        public int Add(PeriyodikBakimBildirimPushed periyodikbakimbildirimpushed)
        {
            return _periyodikbakimbildirimpushedDal.Add(periyodikbakimbildirimpushed);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, PeriyodikBakimBildirimPushedUpdate")]
        public int Update(PeriyodikBakimBildirimPushed periyodikbakimbildirimpushed)
        {
            return _periyodikbakimbildirimpushedDal.Update(periyodikbakimbildirimpushed);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, PeriyodikBakimBildirimPushedDelete")]
        public int Delete(int Id)
        {
            return _periyodikbakimbildirimpushedDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, PeriyodikBakimBildirimPushedDelete")]
        public int DeleteSoft(int Id)
        {
            return _periyodikbakimbildirimpushedDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, PeriyodikBakimBildirimPushedRead, PeriyodikBakimBildirimPushedLtd")]
        public List<PeriyodikBakimBildirimPushed> GetListPagination(PagingParams pagingParams)
        {
            return _periyodikbakimbildirimpushedDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _periyodikbakimbildirimpushedDal.GetCount(filter);
        }

    }

}