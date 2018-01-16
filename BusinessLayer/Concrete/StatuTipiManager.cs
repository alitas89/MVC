using System.Collections.Generic;
using BusinessLayer.Abstract;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class StatuTipiManager : IStatuTipiService
    {
        IStatuTipiDal _statutipiDal;

        public StatuTipiManager(IStatuTipiDal statutipiDal)
        {
            _statutipiDal = statutipiDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<StatuTipi> GetList()
        {
            return _statutipiDal.GetList();
        }
        [SecuredOperation(Roles = "Admin,Editor")]
        public StatuTipi GetById(int Id)
        {
            return _statutipiDal.Get(Id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(StatuTipi statutipi)
        {
            return _statutipiDal.Add(statutipi);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(StatuTipi statutipi)
        {
            return _statutipiDal.Update(statutipi);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _statutipiDal.Delete(Id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _statutipiDal.DeleteSoft(Id);
        }
    }
}