using System.Collections.Generic;
using BusinessLayer.Abstract.Satinalma;
using DataAccessLayer.Abstract.Satinalma;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Aspects.Postsharp.CacheAspects;
using EntityLayer.Concrete.Satinalma;
using Core.Aspects.Postsharp.AuthorizationAspects;
using EntityLayer.ComplexTypes.DtoModel.SatinAlma;
using EntityLayer.ComplexTypes.ParameterModel;

namespace BusinessLayer.Concrete.Satinalma
{
    public class TeslimYeriManager : ITeslimYeriService
    {
        ITeslimYeriDal _teslimyeriDal;

        public TeslimYeriManager(ITeslimYeriDal teslimyeriDal)
        {
            _teslimyeriDal = teslimyeriDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<TeslimYeri> GetList()
        {
            return _teslimyeriDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public TeslimYeri GetById(int Id)
        {
            return _teslimyeriDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(TeslimYeri teslimyeri)
        {
            return _teslimyeriDal.Add(teslimyeri);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(TeslimYeri teslimyeri)
        {
            return _teslimyeriDal.Update(teslimyeri);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _teslimyeriDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _teslimyeriDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<TeslimYeri> GetListPagination(PagingParams pagingParams)
        {
            return _teslimyeriDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _teslimyeriDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<TeslimYeriDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _teslimyeriDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _teslimyeriDal.GetCountDto(filter);
        }
    }
}
