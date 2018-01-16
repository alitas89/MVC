using System;
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
    public class RiskTipiManager : IRiskTipiService
    {
        IRiskTipiDal _risktipiDal;

        public RiskTipiManager(IRiskTipiDal risktipiDal)
        {
            _risktipiDal = risktipiDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<RiskTipi> GetList()
        {
            return _risktipiDal.GetList();
        }
        [SecuredOperation(Roles = "Admin,Editor")]
        public RiskTipi GetById(int Id)
        {
            return _risktipiDal.Get(Id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(RiskTipi risktipi)
        {
            return _risktipiDal.Add(risktipi);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(RiskTipi risktipi)
        {
            return _risktipiDal.Update(risktipi);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _risktipiDal.Delete(Id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _risktipiDal.DeleteSoft(Id);
        }
    }
}
