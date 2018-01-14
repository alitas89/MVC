﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class IsTipiManager : IIsTipiService
    {
        IIsTipiDal _isTipiDal;

        public IsTipiManager(IIsTipiDal isTipiDal)
        {
            _isTipiDal = isTipiDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<IsTipi> GetList()
        {
            return _isTipiDal.GetList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<IsTipiDto> GetListDto()
        {
            return _isTipiDal.GetListDto();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public IsTipi GetById(int Id)
        {
            return _isTipiDal.Get(Id);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(IsTipi ıstipi)
        {
            return _isTipiDal.Add(ıstipi);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(IsTipi ıstipi)
        {
            return _isTipiDal.Update(ıstipi);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _isTipiDal.Delete(Id);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _isTipiDal.DeleteSoft(Id);
        }
    }
}
