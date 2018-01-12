using System.Collections.Generic;
using BusinessLayer.Abstract;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class BakimRiskiManager : IBakimRiskiService
    {
        IBakimRiskiDal _bakimriskiDal;

        public BakimRiskiManager(IBakimRiskiDal bakimriskiDal)
        {
            _bakimriskiDal = bakimriskiDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<BakimRiski> GetList()
        {
            return _bakimriskiDal.GetList();
        }
        [SecuredOperation(Roles = "Admin,Editor")]
        public BakimRiski GetById(int Id)
        {
            return _bakimriskiDal.Get(Id);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(BakimRiski bakimriski)
        {
            return _bakimriskiDal.Add(bakimriski);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(BakimRiski bakimriski)
        {
            return _bakimriskiDal.Update(bakimriski);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _bakimriskiDal.Delete(Id);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _bakimriskiDal.DeleteSoft(Id);
        }
    }
}