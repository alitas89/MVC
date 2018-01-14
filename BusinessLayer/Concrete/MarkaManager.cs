﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules.FluentValidation;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.Aspects.Postsharp.ValidationAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class MarkaManager : IMarkaService
    {
        IMarkaDal _markaDal;

        public MarkaManager(IMarkaDal markaDal)
        {
            _markaDal = markaDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<Marka> GetList()
        {
            return _markaDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public Marka GetById(int Id)
        {
            return _markaDal.Get(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [FluentValidationAspect(typeof(MarkaValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(Marka marka)
        {
            return _markaDal.Add(marka);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [FluentValidationAspect(typeof(MarkaValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(Marka marka)
        {
            return _markaDal.Update(marka);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _markaDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _markaDal.DeleteSoft(Id);
        }
    }
}