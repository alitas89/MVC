using System.Collections.Generic;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class IsEmriManager : IIsEmriService
    {
        IIsEmriDal _isEmriDal;

        public IsEmriManager(IIsEmriDal ısemriDal)
        {
            _isEmriDal = ısemriDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<IsEmri> GetList()
        {
            return _isEmriDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public IsEmri GetById(int Id)
        {
            return _isEmriDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(IsEmri ısemri)
        {
            return _isEmriDal.Add(ısemri);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(IsEmri ısemri)
        {
            return _isEmriDal.Update(ısemri);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _isEmriDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _isEmriDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<IsEmri> GetListPagination(PagingParams pagingParams)
        {
            return _isEmriDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _isEmriDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<IsEmriDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _isEmriDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _isEmriDal.GetCountDto(filter);
        }

    }

}