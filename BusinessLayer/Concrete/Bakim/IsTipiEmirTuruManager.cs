﻿using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BusinessLayer.Concrete.Bakim
{
    public class IsTipiEmirTuruManager : IIsTipiEmirTuruService
    {
        IIsTipiEmirTuruDal _isTipiEmirTuruDal;

        public IsTipiEmirTuruManager(IIsTipiEmirTuruDal isTipiEmirTuruDal)
        {
            _isTipiEmirTuruDal = isTipiEmirTuruDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IsTipiEmirTuruRead, IsTipiEmirTuruLtd")]
        public List<IsTipiEmirTuru> GetList()
        {
            return _isTipiEmirTuruDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, IsTipiEmirTuruRead, IsTipiEmirTuruLtd")]
        public IsTipiEmirTuru GetById(int Id)
        {
            return _isTipiEmirTuruDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IsTipiEmirTuruCreate")]
        public int Add(IsTipiEmirTuru isTipiEmirTuru)
        {
            return _isTipiEmirTuruDal.Add(isTipiEmirTuru);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IsTipiEmirTuruUpdate")]
        public int Update(IsTipiEmirTuru isTipiEmirTuru)
        {
            return _isTipiEmirTuruDal.Update(isTipiEmirTuru);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IsTipiEmirTuruDelete")]
        public int Delete(int Id)
        {
            return _isTipiEmirTuruDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IsTipiEmirTuruDelete")]
        public int DeleteSoft(int Id)
        {
            return _isTipiEmirTuruDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, IsTipiEmirTuruRead, IsTipiEmirTuruLtd")]
        public List<IsTipiEmirTuru> GetListPagination(PagingParams pagingParams)
        {
            return _isTipiEmirTuruDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _isTipiEmirTuruDal.GetCount(filter);
        }

        
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IsTipiEmirTuruCreate")]
        public int AddIsTipiDetay(IsTipiTemp isTipiTemp)
        {
            var listIsTipiEmirTuru = JsonConvert.DeserializeObject<List<IsTipiEmirTuru>>(isTipiTemp.arrIsEmriTuru);

            var count = _isTipiEmirTuruDal.AddWithTransaction(isTipiTemp.IsTipiID, listIsTipiEmirTuru);

            return count;
        }


        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IsTipiEmirTuruUpdate")]
        public int UpdateIsTipiDetay(IsTipiTemp isTipiTemp)
        {
            var listIsTipiEmirTuru = JsonConvert.DeserializeObject<List<IsTipiEmirTuruDto>>(isTipiTemp.arrIsEmriTuru);

            var count = _isTipiEmirTuruDal.UpdateWithTransaction(isTipiTemp.IsTipiID, listIsTipiEmirTuru);

            return count;
        }

        [SecuredOperation(Roles = "Admin, IsTipiRead, IsTipiEmirturuRead")]
        public List<IsTipiEmirTuruDto> GetListPaginationDto(int isTipiID, PagingParams pagingParams)
        {
            return _isTipiEmirTuruDal.GetListPaginationDto(isTipiID, pagingParams);
        }

        public int GetCountDto(int isTipiID, string filter = "")
        {
            return _isTipiEmirTuruDal.GetCountDto(isTipiID, filter);
        }
    }

}
