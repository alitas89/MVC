﻿using System.Collections.Generic;
using BusinessLayer.Abstract;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class BakimOncelikManager : IBakimOncelikService
    {
        IBakimOncelikDal _bakimoncelikDal;

        public BakimOncelikManager(IBakimOncelikDal bakimoncelikDal)
        {
            _bakimoncelikDal = bakimoncelikDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<BakimOncelik> GetList()
        {
            return _bakimoncelikDal.GetList();
        }
        [SecuredOperation(Roles = "Admin,Editor")]
        public BakimOncelik GetById(int Id)
        {
            return _bakimoncelikDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(BakimOncelik bakimoncelik)
        {
            return _bakimoncelikDal.Add(bakimoncelik);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(BakimOncelik bakimoncelik)
        {
            return _bakimoncelikDal.Update(bakimoncelik);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _bakimoncelikDal.Delete(Id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _bakimoncelikDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<BakimOncelik> GetListPagination(PagingParams pagingParams)
        {
            return _bakimoncelikDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filterCol = "", string filterVal = "")
        {
            return _bakimoncelikDal.GetCount(filterCol, filterVal);
        }
    }
}