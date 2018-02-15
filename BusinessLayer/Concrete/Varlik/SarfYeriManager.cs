using System.Collections.Generic;
using BusinessLayer.Abstract.Varlik;
using BusinessLayer.ValidationRules.FluentValidation;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.Aspects.Postsharp.ValidationAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Varlik.DataAccessLayer.Abstract;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Concrete.Varlik
{
    public class SarfYeriManager : ISarfYeriService
    {
        ISarfYeriDal _sarfyeriDal;

        public SarfYeriManager(ISarfYeriDal sarfyeriDal)
        {
            _sarfyeriDal = sarfyeriDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<SarfYeri> GetList()
        {
            return _sarfyeriDal.GetList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<SarfYeri> GetList(int IsletmeID)
        {
            return _sarfyeriDal.GetList(IsletmeID);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<SarfYeriDto> GetListDto()
        {
            return _sarfyeriDal.GetListDto();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public SarfYeri GetById(int Id)
        {
            return _sarfyeriDal.Get(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [FluentValidationAspect(typeof(SarfYeriValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(SarfYeri sarfyeri)
        {
            return _sarfyeriDal.Add(sarfyeri);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [FluentValidationAspect(typeof(SarfYeriValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(SarfYeri sarfyeri)
        {
            return _sarfyeriDal.Update(sarfyeri);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _sarfyeriDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _sarfyeriDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<SarfYeri> GetListPagination(PagingParams pagingParams)
        {
            return _sarfyeriDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filterCol = "", string filterVal = "")
        {
            return _sarfyeriDal.GetCount(filterCol, filterVal);
        }

    }
}
