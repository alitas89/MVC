using System.Collections.Generic;
using BusinessLayer.Abstract;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class ParaBirimManager : IParaBirimService
    {
        IParaBirimDal _parabirimDal;

        public ParaBirimManager(IParaBirimDal parabirimDal)
        {
            _parabirimDal = parabirimDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<ParaBirim> GetList()
        {
            return _parabirimDal.GetList();
        }
        [SecuredOperation(Roles = "Admin,Editor")]
        public ParaBirim GetById(int Id)
        {
            return _parabirimDal.Get(Id);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(ParaBirim parabirim)
        {
            return _parabirimDal.Add(parabirim);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(ParaBirim parabirim)
        {
            return _parabirimDal.Update(parabirim);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _parabirimDal.Delete(Id);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _parabirimDal.DeleteSoft(Id);
        }
    }
}