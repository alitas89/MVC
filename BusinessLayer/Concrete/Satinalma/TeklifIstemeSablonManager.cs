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
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<TeklifIstemeSablon> GetList()
        {
            return _teklifIstemesablonDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public TeklifIstemeSablon GetById(int Id)
        {
            return _teklifIstemesablonDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(TeklifIstemeSablon teklifıstemesablon)
        {
            return _teklifIstemesablonDal.Add(teklifıstemesablon);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(TeklifIstemeSablon teklifıstemesablon)
        {
            return _teklifIstemesablonDal.Update(teklifıstemesablon);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _teklifIstemesablonDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _teklifIstemesablonDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<TeklifIstemeSablon> GetListPagination(PagingParams pagingParams)
        {
            return _teklifIstemesablonDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filterCol = "", string filterVal = "")
        {
            return _teklifIstemesablonDal.GetCount(filterCol, filterVal);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<TeklifIstemeSablonDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _teklifIstemesablonDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filterCol = "", string filterVal = "")
        {
            return _teklifIstemesablonDal.GetCountDto(filterCol, filterVal);
        }
    }
}

