using System.Collections.Generic;
using BusinessLayer.Abstract.Varlik;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;

namespace BusinessLayer.Concrete.Varlik
{
    public class VarlikManager : IVarlikService
    {
        IVarlikDal _varlikDal;

        public VarlikManager(IVarlikDal varlikDal)
        {
            _varlikDal = varlikDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<EntityLayer.Concrete.Varlik.Varlik> GetList()
        {
            return _varlikDal.GetList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<EntityLayer.Concrete.Varlik.Varlik> GetList(int KisimID)
        {
            return _varlikDal.GetList(KisimID);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public EntityLayer.Concrete.Varlik.Varlik GetById(int Id)
        {
            return _varlikDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(EntityLayer.Concrete.Varlik.Varlik varlik)
        {
            //Kod Kontrolü - Aynı koda sahip kayıt varsa ekleme yapılamaz!
            return _varlikDal.IsKodDefined(varlik.Kod) ? 0 : _varlikDal.Add(varlik);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(EntityLayer.Concrete.Varlik.Varlik varlik)
        {
            //Kod Kontrolü - Aynı koda sahip kayıt varsa güncelleme yapılamaz! (Kendisi dışındaki bir kod olmalı)
            if (_varlikDal.IsKodDefined(varlik.Kod))
            {
                //Var olan kod kendi kodu mu?
                return _varlikDal.Get(varlik.VarlikID).Kod == varlik.Kod ? _varlikDal.Update(varlik) : 0;
            }
            else
            {
                return _varlikDal.Update(varlik);
            }
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _varlikDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _varlikDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<EntityLayer.Concrete.Varlik.Varlik> GetListPagination(PagingParams pagingParams)
        {
            return _varlikDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _varlikDal.GetCount(filter);
        }

        public int GetCountDto(string filter = "")
        {
            return _varlikDal.GetCountDto(filter);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<VarlikDto> GetListDto()
        {
            return _varlikDal.GetListDto();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<VarlikDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _varlikDal.GetListPaginationDto(pagingParams);
        }
    }
}