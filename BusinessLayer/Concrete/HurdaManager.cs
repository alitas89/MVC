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
    public class HurdaManager : IHurdaService
    {
        IHurdaDal _hurdaDal;

        public HurdaManager(IHurdaDal hurdaDal)
        {
            _hurdaDal = hurdaDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<Hurda> GetList()
        {
            return _hurdaDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public Hurda GetById(int Id)
        {
            return _hurdaDal.Get(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(DurusNedeniValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(Hurda hurda)
        {
            return _hurdaDal.Add(hurda);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        //[FluentValidationAspect(typeof(DurusNedeniValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(Hurda hurda)
        {
            return _hurdaDal.Update(hurda);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _hurdaDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _hurdaDal.DeleteSoft(Id);
        }
    }
}
