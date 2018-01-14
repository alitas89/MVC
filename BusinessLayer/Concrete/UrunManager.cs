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
    public class UrunManager : IUrunService
    {
        IUrunDal _urunDal;

        public UrunManager(IUrunDal urunDal)
        {
            _urunDal = urunDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<Urun> GetList()
        {
            return _urunDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public Urun GetById(int Id)
        {
            return _urunDal.Get(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(DurusNedeniValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(Urun urun)
        {
            return _urunDal.Add(urun);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(DurusNedeniValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(Urun urun)
        {
            return _urunDal.Update(urun);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _urunDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _urunDal.DeleteSoft(Id);
        }
    }
}