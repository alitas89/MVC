using System.Collections.Generic;
using BusinessLayer.Abstract.Satinalma;
using DataAccessLayer.Abstract.Satinalma;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Aspects.Postsharp.CacheAspects;
using Core.Aspects.Postsharp.AuthorizationAspects;
using EntityLayer.Concrete.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;

namespace BusinessLayer.Concrete.Satinalma
{
    public class BelgeTuruManager : IBelgeTuruService
    {
        IBelgeTuruDal _belgeturuDal;

        public BelgeTuruManager(IBelgeTuruDal belgeturuDal)
        {
            _belgeturuDal = belgeturuDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SatinAlmaRead, BelgeTuruRead, BelgeTuruLtd")]
        public List<BelgeTuru> GetList()
        {
            return _belgeturuDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, SatinAlmaRead, BelgeTuruRead, BelgeTuruLtd")]
        public BelgeTuru GetById(int Id)
        {
            return _belgeturuDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SatinAlmaCreate, BelgeTuruCreate")]
        public int Add(BelgeTuru belgeturu)
        {
            return _belgeturuDal.Add(belgeturu);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SatinAlmaUpdate, BelgeTuruUpdate")]
        public int Update(BelgeTuru belgeturu)
        {
            return _belgeturuDal.Update(belgeturu);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SatinAlmaDelete, BelgeTuruDelete")]
        public int Delete(int Id)
        {
            return _belgeturuDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SatinAlmaDelete, BelgeTuruDelete")]
        public int DeleteSoft(int Id)
        {
            return _belgeturuDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, SatinAlmaRead, BelgeTuruRead, BelgeTuruLtd")]
        public List<BelgeTuru> GetListPagination(PagingParams pagingParams)
        {
            return _belgeturuDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _belgeturuDal.GetCount(filter);
        }

    }
}