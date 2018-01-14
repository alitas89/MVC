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
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class VarlikDurumuManager : IVarlikDurumuService
    {
        IVarlikDurumuDal _varlikdurumuDal;

        public VarlikDurumuManager(IVarlikDurumuDal varlikdurumuDal)
        {
            _varlikdurumuDal = varlikdurumuDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<VarlikDurumu> GetList()
        {
            return _varlikdurumuDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public VarlikDurumu GetById(int Id)
        {
            return _varlikdurumuDal.Get(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(DurusNedeniValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(VarlikDurumu varlikdurumu)
        {
            return _varlikdurumuDal.Add(varlikdurumu);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(DurusNedeniValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(VarlikDurumu varlikdurumu)
        {
            return _varlikdurumuDal.Update(varlikdurumu);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _varlikdurumuDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _varlikdurumuDal.DeleteSoft(Id);
        }
    }
}