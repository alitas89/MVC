using System.Collections.Generic;
using BusinessLayer.Abstract.Varlik;
using BusinessLayer.ValidationRules.FluentValidation;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.Aspects.Postsharp.ValidationAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Concrete.Varlik
{
    public class VarlikGrupManager : IVarlikGrupService
    {
        IVarlikGrupDal _varlikgrupDal;

        public VarlikGrupManager(IVarlikGrupDal varlikgrupDal)
        {
            _varlikgrupDal = varlikgrupDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikGrupRead, VarlikGrupLtd")]
        public List<VarlikGrup> GetList()
        {
            return _varlikgrupDal.GetList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikGrupRead, VarlikGrupLtd")]
        public List<VarlikGrupDto> GetListDto()
        {
            return _varlikgrupDal.GetListDto();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikGrupRead, VarlikGrupLtd")]
        public VarlikGrup GetById(int Id)
        {
            return _varlikgrupDal.Get(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [FluentValidationAspect(typeof(VarlikGrupValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin, VarlikCreate, VarlikGrupCreate")]
        public int Add(VarlikGrup varlikgrup)
        {
            return _varlikgrupDal.Add(varlikgrup);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [FluentValidationAspect(typeof(VarlikGrupValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin, VarlikUpdate, VarlikGrupUpdate")]
        public int Update(VarlikGrup varlikgrup)
        {
            return _varlikgrupDal.Update(varlikgrup);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikDelete, VarlikGrupDelete")]
        public int Delete(int Id)
        {
            return _varlikgrupDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikDelete, VarlikGrupDelete")]
        public int DeleteSoft(int Id)
        {
            return _varlikgrupDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikGrupRead, VarlikGrupLtd")]
        public List<VarlikGrup> GetListPagination(PagingParams pagingParams)
        {
            return _varlikgrupDal.GetListPagination(pagingParams);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikGrupRead, VarlikGrupLtd")]
        public List<VarlikGrupDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _varlikgrupDal.GetListPaginationDto(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _varlikgrupDal.GetCount(filter);
        }

        public int GetCountDto(string filter = "")
        {
            return _varlikgrupDal.GetCountDto(filter);
        }
    }
}
