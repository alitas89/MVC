using System.Collections.Generic;
using BusinessLayer.Abstract;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class UretimTipiManager : IUretimTipiService
    {
        IUretimTipiDal _uretimtipiDal;

        public UretimTipiManager(IUretimTipiDal uretimtipiDal)
        {
            _uretimtipiDal = uretimtipiDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<UretimTipi> GetList()
        {
            return _uretimtipiDal.GetList();
        }
        [SecuredOperation(Roles = "Admin,Editor")]
        public UretimTipi GetById(int Id)
        {
            return _uretimtipiDal.Get(Id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(UretimTipi uretimtipi)
        {
            return _uretimtipiDal.Add(uretimtipi);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(UretimTipi uretimtipi)
        {
            return _uretimtipiDal.Update(uretimtipi);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _uretimtipiDal.Delete(Id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _uretimtipiDal.DeleteSoft(Id);
        }
    }
}