﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract.Personel;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace BusinessLayer.Concrete.Personel
{
    public class MesaiTuruManager : IMesaiTuruService
    {
        IMesaiTuruDal _mesaituruDal;

        public MesaiTuruManager(IMesaiTuruDal mesaituruDal)
        {
            _mesaituruDal = mesaituruDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<MesaiTuru> GetList()
        {
            return _mesaituruDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public MesaiTuru GetById(int Id)
        {
            return _mesaituruDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(MesaiTuru mesaituru)
        {
            return _mesaituruDal.Add(mesaituru);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(MesaiTuru mesaituru)
        {
            return _mesaituruDal.Update(mesaituru);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _mesaituruDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _mesaituruDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<MesaiTuru> GetListPagination(PagingParams pagingParams)
        {
            return _mesaituruDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filterCol = "", string filterVal = "")
        {
            return _mesaituruDal.GetCount(filterCol, filterVal);
        }

    }
}