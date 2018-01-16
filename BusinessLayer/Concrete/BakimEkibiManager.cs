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
    public class BakimEkibiManager : IBakimEkibiService
    {
        IBakimEkibiDal _bakimekibiDal;

        public BakimEkibiManager(IBakimEkibiDal bakimekibiDal)
        {
            _bakimekibiDal = bakimekibiDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<BakimEkibi> GetList()
        {
            return _bakimekibiDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public BakimEkibi GetById(int Id)
        {
            return _bakimekibiDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(BakimEkibi bakimekibi)
        {
            return _bakimekibiDal.Add(bakimekibi);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(BakimEkibi bakimekibi)
        {
            return _bakimekibiDal.Update(bakimekibi);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _bakimekibiDal.Delete(Id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _bakimekibiDal.DeleteSoft(Id);
        }
    }
}
