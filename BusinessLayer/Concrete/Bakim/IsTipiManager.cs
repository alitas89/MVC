﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class IsTipiManager : IIsTipiService
    {
        IIsTipiDal _isTipiDal;

        public IsTipiManager(IIsTipiDal isTipiDal)
        {
            _isTipiDal = isTipiDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IsTipiRead, IsTipiLtd")]
        public List<IsTipi> GetList()
        {
            return _isTipiDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, IsTipiRead, IsTipiLtd")]
        public IsTipi GetById(int Id)
        {
            return _isTipiDal.Get(Id);
        }
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IsTipiCreate")]
        public int Add(IsTipi ıstipi)
        {
            return _isTipiDal.Add(ıstipi);
        }
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IsTipiUpdate")]
        public int Update(IsTipi ıstipi)
        {
            return _isTipiDal.Update(ıstipi);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IsTipiDelete")]
        public int Delete(int Id)
        {
            return _isTipiDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IsTipiDelete")]
        public int DeleteSoft(int Id)
        {
            return _isTipiDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, IsTipiRead, IsTipiLtd")]
        public List<IsTipi> GetListPagination(PagingParams pagingParams)
        {
            return _isTipiDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _isTipiDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, IsTipiRead, IsTipiLtd")]
        public List<IsTipiDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _isTipiDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _isTipiDal.GetCountDto(filter);
        }
    }
}
