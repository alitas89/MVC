using System.Collections.Generic;
using BusinessLayer.Abstract;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class ArizaNedeniGrubuManager : IArizaNedeniGrubuService
    {
        IArizaNedeniGrubuDal _arizanedenigrubuDal;

        public ArizaNedeniGrubuManager(IArizaNedeniGrubuDal arizanedenigrubuDal)
        {
            _arizanedenigrubuDal = arizanedenigrubuDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<ArizaNedeniGrubu> GetList()
        {
            return _arizanedenigrubuDal.GetList();
        }
        [SecuredOperation(Roles = "Admin,Editor")]
        public ArizaNedeniGrubu GetById(int Id)
        {
            return _arizanedenigrubuDal.Get(Id);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(ArizaNedeniGrubu arizanedenigrubu)
        {
            return _arizanedenigrubuDal.Add(arizanedenigrubu);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(ArizaNedeniGrubu arizanedenigrubu)
        {
            return _arizanedenigrubuDal.Update(arizanedenigrubu);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _arizanedenigrubuDal.Delete(Id);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _arizanedenigrubuDal.DeleteSoft(Id);
        }
    }
}