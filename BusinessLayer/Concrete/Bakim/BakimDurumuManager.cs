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
    public class BakimDurumuManager : IBakimDurumuService
    {
        IBakimDurumuDal _bakimdurumuDal;

        public BakimDurumuManager(IBakimDurumuDal bakimdurumuDal)
        {
            _bakimdurumuDal = bakimdurumuDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimDurumuRead, BakimDurumuLtd, Authorized")]
        public List<BakimDurumu> GetList()
        {
            return _bakimdurumuDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, BakimDurumuRead, BakimDurumuLtd, Authorized")]
        public BakimDurumu GetById(int Id)
        {
            return _bakimdurumuDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimDurumuCreate")]
        public int Add(BakimDurumu bakimdurumu)
        {
            return _bakimdurumuDal.Add(bakimdurumu);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimDurumuUpdate")]
        public int Update(BakimDurumu bakimdurumu)
        {
            return _bakimdurumuDal.Update(bakimdurumu);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimDurumuDelete")]
        public int Delete(int Id)
        {
            return _bakimdurumuDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimDurumuDelete")]
        public int DeleteSoft(int Id)
        {
            return _bakimdurumuDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimDurumuRead, BakimDurumuLtd")]
        public List<BakimDurumu> GetListPagination(PagingParams pagingParams)
        {
            return _bakimdurumuDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _bakimdurumuDal.GetCount(filter);
        }

    }
}