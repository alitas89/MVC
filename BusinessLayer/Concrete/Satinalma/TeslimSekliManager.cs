using System.Collections.Generic;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using BusinessLayer.Abstract.Satinalma;
using DataAccessLayer.Abstract.Satinalma;
using Core.Aspects.Postsharp.CacheAspects;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using EntityLayer.Concrete.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;

namespace BusinessLayer.Concrete.Satinalma
{
    public class TeslimSekliManager : ITeslimSekliService
    {
        ITeslimSekliDal _teslimSekliDal;

        public TeslimSekliManager(ITeslimSekliDal teslimSekliDal)
        {
            _teslimSekliDal = teslimSekliDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SatinAlmaRead, TeslimSekliRead, TeslimSekliLtd")]
        public List<TeslimSekli> GetList()
        {
            return _teslimSekliDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, SatinAlmaRead, TeslimSekliRead, TeslimSekliLtd")]
        public TeslimSekli GetById(int Id)
        {
            return _teslimSekliDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SatinAlmaCreate, TeslimSekliCreate")]
        public int Add(TeslimSekli teslimsekli)
        {
            return _teslimSekliDal.Add(teslimsekli);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SatinAlmaUpdate, TeslimSekliUpdate")]
        public int Update(TeslimSekli teslimsekli)
        {
            return _teslimSekliDal.Update(teslimsekli);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SatinAlmaDelete, TeslimSekliDelete")]
        public int Delete(int Id)
        {
            return _teslimSekliDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SatinAlmaDelete, TeslimSekliDelete")]
        public int DeleteSoft(int Id)
        {
            return _teslimSekliDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, SatinAlmaRead, TeslimSekliRead, TeslimSekliLtd")]
        public List<TeslimSekli> GetListPagination(PagingParams pagingParams)
        {
            return _teslimSekliDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _teslimSekliDal.GetCount(filter);
        }

    }
}
