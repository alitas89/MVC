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
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace BusinessLayer.Concrete.Malzeme
{
    public class AmbarManager : IAmbarService
    {
        IAmbarDal _ambarDal;

        public AmbarManager(IAmbarDal ambarDal)
        {
            _ambarDal = ambarDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, AmbarRead, AmbarLtd")]
        public List<Ambar> GetList()
        {
            return _ambarDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, AmbarRead, AmbarLtd")]
        public Ambar GetById(int Id)
        {
            return _ambarDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, AmbarCreate")]
        public int Add(Ambar ambar)
        {
            return _ambarDal.Add(ambar);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, AmbarUpdate")]
        public int Update(Ambar ambar)
        {
            return _ambarDal.Update(ambar);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, AmbarDelete")]
        public int Delete(int Id)
        {
            return _ambarDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, AmbarDelete")]
        public int DeleteSoft(int Id)
        {
            return _ambarDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, AmbarRead, AmbarLtd")]
        public List<Ambar> GetListPagination(PagingParams pagingParams)
        {
            return _ambarDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _ambarDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, AmbarRead, AmbarLtd")]
        public List<AmbarDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _ambarDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _ambarDal.GetCountDto(filter);
        }

    }
}
