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
    public class KisimManager : IKisimService
    {
        IKisimDal _kisimDal;

        public KisimManager(IKisimDal kisimDal)
        {
            _kisimDal = kisimDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<Kisim> GetList()
        {
            return _kisimDal.GetList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<Kisim> GetList(int SarfYeriID)
        {
            return _kisimDal.GetList(SarfYeriID);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<KisimDto> GetListDto()
        {
            return _kisimDal.GetListDto();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public Kisim GetById(int Id)
        {
            return _kisimDal.Get(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [FluentValidationAspect(typeof(KisimValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(Kisim kisim)
        {            
            //Kod Kontrolü - Aynı koda sahip kayıt varsa ekleme yapılamaz!
            return _kisimDal.IsKodDefined(kisim.Kod) ? 0 : _kisimDal.Add(kisim);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [FluentValidationAspect(typeof(KisimValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(Kisim kisim)
        {
            //Kod Kontrolü - Aynı koda sahip kayıt varsa güncelleme yapılamaz!
            return _kisimDal.IsKodDefined(kisim.Kod) ? 0 : _kisimDal.Update(kisim);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _kisimDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _kisimDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<Kisim> GetListPagination(PagingParams pagingParams)
        {
            return _kisimDal.GetListPagination(pagingParams);
        }
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<KisimDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _kisimDal.GetListPaginationDto(pagingParams);
        }
        public int GetCount(string filterCol = "", string filterVal = "")
        {
            return _kisimDal.GetCount(filterCol, filterVal);
        }

        public int GetCountDto(string filterCol = "", string filterVal = "")
        {
            return _kisimDal.GetCountDto(filterCol, filterVal);
        }
    }
}
