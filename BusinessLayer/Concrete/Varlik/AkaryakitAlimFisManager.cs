using BusinessLayer.Abstract.Varlik;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Varlik;

namespace BusinessLayer.Concrete.Varlik
{
    public class AkaryakitAlimFisManager : IAkaryakitAlimFisService
    {
        IAkaryakitAlimFisDal _akaryakitalimfisDal;

        public AkaryakitAlimFisManager(IAkaryakitAlimFisDal akaryakitalimfisDal)
        {
            _akaryakitalimfisDal = akaryakitalimfisDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikRead, AkaryakitAlimFisRead, AkaryakitAlimFisLtd")]
        public List<AkaryakitAlimFis> GetList()
        {
            return _akaryakitalimfisDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, AkaryakitAlimFisRead, AkaryakitAlimFisLtd")]
        public AkaryakitAlimFis GetById(int Id)
        {
            return _akaryakitalimfisDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikCreate, AkaryakitAlimFisCreate")]
        public int Add(AkaryakitAlimFis akaryakitalimfis)
        {
            return _akaryakitalimfisDal.Add(akaryakitalimfis);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikUpdate, AkaryakitAlimFisUpdate")]
        public int Update(AkaryakitAlimFis akaryakitalimfis)
        {
            return _akaryakitalimfisDal.Update(akaryakitalimfis);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikDelete, AkaryakitAlimFisDelete")]
        public int Delete(int Id)
        {
            return _akaryakitalimfisDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikDelete, AkaryakitAlimFisDelete")]
        public int DeleteSoft(int Id)
        {
            return _akaryakitalimfisDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, AkaryakitAlimFisRead, AkaryakitAlimFisLtd")]
        public List<AkaryakitAlimFis> GetListPagination(PagingParams pagingParams)
        {
            return _akaryakitalimfisDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _akaryakitalimfisDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, AkaryakitAlimFisRead, AkaryakitAlimFisLtd")]
        public List<AkaryakitAlimFisDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _akaryakitalimfisDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _akaryakitalimfisDal.GetCountDto(filter);
        }
    }
}
