﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract.Malzeme;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace BusinessLayer.Concrete.Malzeme
{
    public class OlcuBirimManager : IOlcuBirimService
    {
        IOlcuBirimDal _olcubirimDal;

        public OlcuBirimManager(IOlcuBirimDal olcubirimDal)
        {
            _olcubirimDal = olcubirimDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<OlcuBirim> GetList()
        {
            return _olcubirimDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public OlcuBirim GetById(int Id)
        {
            return _olcubirimDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(OlcuBirim olcubirim)
        {
            return _olcubirimDal.Add(olcubirim);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(OlcuBirim olcubirim)
        {
            return _olcubirimDal.Update(olcubirim);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _olcubirimDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _olcubirimDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<OlcuBirim> GetListPagination(PagingParams pagingParams)
        {
            return _olcubirimDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filterCol = "", string filterVal = "")
        {
            return _olcubirimDal.GetCount(filterCol, filterVal);
        }

    }
}