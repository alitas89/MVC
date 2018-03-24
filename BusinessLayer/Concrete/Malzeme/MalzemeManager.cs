using BusinessLayer.Abstract.Malzeme;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Malzeme;

namespace BusinessLayer.Concrete.Malzeme
{
    public class MalzemeManager : IMalzemeService
    {
        IMalzemeDal _malzemeDal;

        public MalzemeManager(IMalzemeDal malzemeDal)
        {
            _malzemeDal = malzemeDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemelerRead, MalzemelerLtd")]
        public List<EntityLayer.Concrete.Malzeme.Malzeme> GetList()
        {
            return _malzemeDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemelerRead, MalzemelerLtd")]
        public EntityLayer.Concrete.Malzeme.Malzeme GetById(int Id)
        {
            return _malzemeDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeCreate, MalzemelerCreate")]
        public int Add(EntityLayer.Concrete.Malzeme.Malzeme malzeme)
        {
            return _malzemeDal.IsKodDefined(malzeme.Kod) ? 0 : _malzemeDal.Add(malzeme);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeUpdate, MalzemelerUpdate")]
        public int Update(EntityLayer.Concrete.Malzeme.Malzeme malzeme)
        {
            return _malzemeDal.Update(malzeme);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeDelete, MalzemelerDelete")]
        public int Delete(int Id)
        {
            return _malzemeDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeDelete, MalzemelerDelete")]
        public int DeleteSoft(int Id)
        {
            return _malzemeDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemelerRead, MalzemelerLtd")]
        public List<EntityLayer.Concrete.Malzeme.Malzeme> GetListPagination(PagingParams pagingParams)
        {
            return _malzemeDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _malzemeDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemelerRead, MalzemelerLtd")]
        public List<MalzemeDto> GetListDto()
        {
            return _malzemeDal.GetListDto();
        }
        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemelerRead, MalzemelerLtd")]
        public List<MalzemeDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _malzemeDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _malzemeDal.GetCountDto(filter);
        }
    }
}
