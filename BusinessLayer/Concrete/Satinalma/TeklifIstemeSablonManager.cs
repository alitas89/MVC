using BusinessLayer.Abstract.Satinalma;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Satinalma;
using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.SatinAlma;

namespace BusinessLayer.Concrete.Satinalma
{
    public class TeklifIstemeSablonManager : ITeklifIstemeSablonService
    {
        ITeklifIstemeSablonDal _teklifIstemesablonDal;

        public TeklifIstemeSablonManager(ITeklifIstemeSablonDal teklifIstemesablonDal)
        {
            _teklifIstemesablonDal = teklifIstemesablonDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SatinAlmaRead, TeklifIstemeSablonRead, TeklifIstemeSablonLtd")]
        public List<TeklifIstemeSablon> GetList()
        {
            return _teklifIstemesablonDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, SatinAlmaRead, TeklifIstemeSablonRead, TeklifIstemeSablonLtd")]
        public TeklifIstemeSablon GetById(int Id)
        {
            return _teklifIstemesablonDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SatinAlmaCreate, TeklifIstemeSablonCreate")]
        public int Add(TeklifIstemeSablon teklifıstemesablon)
        {
            return _teklifIstemesablonDal.Add(teklifıstemesablon);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SatinAlmaUpdate, TeklifIstemeSablonUpdate")]
        public int Update(TeklifIstemeSablon teklifıstemesablon)
        {
            return _teklifIstemesablonDal.Update(teklifıstemesablon);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SatinAlmaDelete, TeklifIstemeSablonDelete")]
        public int Delete(int Id)
        {
            return _teklifIstemesablonDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SatinAlmaDelete, TeklifIstemeSablonDelete")]
        public int DeleteSoft(int Id)
        {
            return _teklifIstemesablonDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, SatinAlmaRead, TeklifIstemeSablonRead, TeklifIstemeSablonLtd")]
        public List<TeklifIstemeSablon> GetListPagination(PagingParams pagingParams)
        {
            return _teklifIstemesablonDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _teklifIstemesablonDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, SatinAlmaRead, TeklifIstemeSablonRead, TeklifIstemeSablonLtd")]
        public List<TeklifIstemeSablonDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _teklifIstemesablonDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _teklifIstemesablonDal.GetCountDto(filter);
        }
    }
}

