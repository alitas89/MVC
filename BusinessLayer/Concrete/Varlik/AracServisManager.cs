using BusinessLayer.Abstract.Varlik;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Varlik;

namespace BusinessLayer.Concrete.Varlik
{
    public class AracServisManager : IAracServisService
    {
        IAracServisDal _aracServisDal;

        public AracServisManager(IAracServisDal aracservisDal)
        {
            _aracServisDal = aracservisDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikRead, AracServisRead, AracServisLtd")]
        public List<AracServis> GetList()
        {
            return _aracServisDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, AracServisRead, AracServisLtd")]
        public AracServis GetById(int Id)
        {
            return _aracServisDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikCreate, AracServisCreate")]
        public int Add(AracServis aracservis)
        {
            return _aracServisDal.Add(aracservis);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikUpdate, AracServisUpdate")]
        public int Update(AracServis aracservis)
        {
            return _aracServisDal.Update(aracservis);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikDelete, AracServisDelete")]
        public int Delete(int Id)
        {
            return _aracServisDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikDelete, AracServisDelete")]
        public int DeleteSoft(int Id)
        {
            return _aracServisDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, AracServisRead, AracServisLtd")]
        public List<AracServis> GetListPagination(PagingParams pagingParams)
        {
            return _aracServisDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _aracServisDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, AracServisRead, AracServisLtd")]
        public List<AracServisDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _aracServisDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _aracServisDal.GetCountDto(filter);
        }
    }
}
