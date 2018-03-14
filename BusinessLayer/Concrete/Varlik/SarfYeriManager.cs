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
            //Kod Kontrolü - Aynı koda sahip kayıt varsa ekleme yapılamaz!
            return _sarfyeriDal.IsKodDefined(sarfyeri.Kod) ? 0 : _sarfyeriDal.Add(sarfyeri);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [FluentValidationAspect(typeof(SarfYeriValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(SarfYeri sarfyeri)
        {    
            //Kod Kontrolü - Aynı koda sahip kayıt varsa güncelleme yapılamaz! (Kendisi dışındaki bir kod olmalı)
            if (_sarfyeriDal.IsKodDefined(sarfyeri.Kod))
            {
                //Var olan kod kendi kodu mu?
                return _sarfyeriDal.Get(sarfyeri.SarfYeriID).Kod == sarfyeri.Kod ? _sarfyeriDal.Update(sarfyeri) : 0;
            }
            else
            {
                return _sarfyeriDal.Update(sarfyeri);
            }
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

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<SarfYeriDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _sarfyeriDal.GetListPaginationDto(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _sarfyeriDal.GetCount(filter);
        }

        public int GetCountDto(string filter = "")
        {
            return _sarfyeriDal.GetCountDto(filter);
        }
    }
}
