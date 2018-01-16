using System.Collections.Generic;
using BusinessLayer.Abstract;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class GonderimFormatiManager : IGonderimFormatiService
    {
        IGonderimFormatiDal _gonderimformatiDal;

        public GonderimFormatiManager(IGonderimFormatiDal gonderimformatiDal)
        {
            _gonderimformatiDal = gonderimformatiDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<GonderimFormati> GetList()
        {
            return _gonderimformatiDal.GetList();
        }
        [SecuredOperation(Roles = "Admin,Editor")]
        public GonderimFormati GetById(int Id)
        {
            return _gonderimformatiDal.Get(Id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(GonderimFormati gonderimformati)
        {
            return _gonderimformatiDal.Add(gonderimformati);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(GonderimFormati gonderimformati)
        {
            return _gonderimformatiDal.Update(gonderimformati);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _gonderimformatiDal.Delete(Id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _gonderimformatiDal.DeleteSoft(Id);
        }
    }
}