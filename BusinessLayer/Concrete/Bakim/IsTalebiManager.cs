using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class IsTalebiManager : IIsTalebiService
    {
        IIsTalebiDal _isTalebiDal;

        public IsTalebiManager(IIsTalebiDal ıstalebiDal)
        {
            _isTalebiDal = ıstalebiDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IsTalebiRead, IsTalebiLtd")]
        public List<IsTalebi> GetList()
        {
            return _isTalebiDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, IsTalebiRead, IsTalebiLtd")]
        public IsTalebi GetById(int Id)
        {
            return _isTalebiDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IsTalebiCreate")]
        public int Add(IsTalebi ıstalebi)
        {
            return _isTalebiDal.Add(ıstalebi);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IsTalebiUpdate")]
        public int Update(IsTalebi istalebi)
        {
            return _isTalebiDal.Update(istalebi);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IsTalebiDelete")]
        public int Delete(int Id)
        {
            return _isTalebiDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IsTalebiDelete")]
        public int DeleteSoft(int Id)
        {
            return _isTalebiDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, IsTalebiRead, IsTalebiLtd")]
        public List<IsTalebi> GetListPagination(PagingParams pagingParams)
        {
            return _isTalebiDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _isTalebiDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, IsTalebiRead, IsTalebiLtd")]
        public List<IsTalebiDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _isTalebiDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _isTalebiDal.GetCountDto(filter);
        }
    }
}
