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
    public class BakimArizaKoduManager : IBakimArizaKoduService
    {
        IBakimArizaKoduDal _bakimarizakoduDal;

        public BakimArizaKoduManager(IBakimArizaKoduDal bakimarizakoduDal)
        {
            _bakimarizakoduDal = bakimarizakoduDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<BakimArizaKodu> GetList()
        {
            return _bakimarizakoduDal.GetList();
        }
        [SecuredOperation(Roles = "Admin,Editor")]
        public BakimArizaKodu GetById(int Id)
        {
            return _bakimarizakoduDal.Get(Id);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(BakimArizaKodu bakimarizakodu)
        {
            return _bakimarizakoduDal.Add(bakimarizakodu);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(BakimArizaKodu bakimarizakodu)
        {
            return _bakimarizakoduDal.Update(bakimarizakodu);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _bakimarizakoduDal.Delete(Id);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _bakimarizakoduDal.DeleteSoft(Id);
        }
    }
}
