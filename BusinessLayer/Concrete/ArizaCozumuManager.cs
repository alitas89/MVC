using System.Collections.Generic;
using BusinessLayer.Abstract;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class ArizaCozumuManager : IArizaCozumuService
    {
        IArizaCozumuDal _arizacozumuDal;

        public ArizaCozumuManager(IArizaCozumuDal arizacozumuDal)
        {
            _arizacozumuDal = arizacozumuDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<ArizaCozumu> GetList()
        {
            return _arizacozumuDal.GetList();
        }
        [SecuredOperation(Roles = "Admin,Editor")]
        public ArizaCozumu GetById(int Id)
        {
            return _arizacozumuDal.Get(Id);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(ArizaCozumu arizacozumu)
        {
            return _arizacozumuDal.Add(arizacozumu);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(ArizaCozumu arizacozumu)
        {
            return _arizacozumuDal.Update(arizacozumu);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _arizacozumuDal.Delete(Id);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _arizacozumuDal.DeleteSoft(Id);
        }
    }
}