using System.Collections.Generic;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class IsAdimlariManager : IIsAdimlariService
    {
        IIsAdimlariDal _ısadimlariDal;

        public IsAdimlariManager(IIsAdimlariDal ısadimlariDal)
        {
            _ısadimlariDal = ısadimlariDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimRead, BakimPlaniRead, IsAdimlariRead, IsAdimlariLtd")]
        public List<IsAdimlari> GetList()
        {
            return _ısadimlariDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, BakimRead, BakimPlaniRead, IsAdimlariRead, IsAdimlariLtd")]
        public IsAdimlari GetById(int Id)
        {
            return _ısadimlariDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimCreate, BakimPlaniCreate, IsAdimlariCreate")]
        public int Add(IsAdimlari ısadimlari)
        {
            return _ısadimlariDal.Add(ısadimlari);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimUpdate, BakimPlaniUpdate, IsAdimlariUpdate")]
        public int Update(IsAdimlari ısadimlari)
        {
            return _ısadimlariDal.Update(ısadimlari);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimDelete, BakimPlaniDelete, IsAdimlariDelete")]
        public int Delete(int Id)
        {
            return _ısadimlariDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimDelete, BakimPlaniDelete, IsAdimlariDelete")]
        public int DeleteSoft(int Id)
        {
            return _ısadimlariDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, IsAdimlariRead, BakimPlaniRead, IsAdimlariLtd")]
        public List<IsAdimlari> GetListPagination(PagingParams pagingParams)
        {
            return _ısadimlariDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _ısadimlariDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, IsAdimlariRead, BakimPlaniRead, IsAdimlariLtd")]
        public List<IsAdimlari> GetListIsAdimlariByBakimPlaniID(int BakimPlaniID)
        {
            return _ısadimlariDal.GetListIsAdimlariByBakimPlaniID(BakimPlaniID);
        }

    }

}