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

        
        [SecuredOperation(Roles = "Admin, SatinAlmaRead, TeslimYeriRead, TeslimYeriLtd")]
        public List<TeslimYeri> GetList()
        {
            return _teslimyeriDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, SatinAlmaRead, TeslimYeriRead, TeslimYeriLtd")]
        public TeslimYeri GetById(int Id)
        {
            return _teslimyeriDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, SatinAlmaCreate, TeslimYeriCreate")]
        public int Add(TeslimYeri teslimyeri)
        {
            return _teslimyeriDal.Add(teslimyeri);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, SatinAlmaUpdate, TeslimYeriUpdate")]
        public int Update(TeslimYeri teslimyeri)
        {
            return _teslimyeriDal.Update(teslimyeri);
        }

        
        [SecuredOperation(Roles = "Admin, SatinAlmaDelete, TeslimYeriDelete")]
        public int Delete(int Id)
        {
            return _teslimyeriDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, SatinAlmaDelete, TeslimYeriDelete")]
        public int DeleteSoft(int Id)
        {
            return _teslimyeriDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, SatinAlmaRead, TeslimYeriRead, TeslimYeriLtd")]
        public List<TeslimYeri> GetListPagination(PagingParams pagingParams)
        {
            return _teslimyeriDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _teslimyeriDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, SatinAlmaRead, TeslimYeriRead, TeslimYeriLtd")]
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
