using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract.Malzeme;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace BusinessLayer.Concrete.Malzeme
{
    public class MuhasebeHesapManager : IMuhasebeHesapService
    {
        IMuhasebeHesapDal _muhasebehesapDal;

        public MuhasebeHesapManager(IMuhasebeHesapDal muhasebehesapDal)
        {
            _muhasebehesapDal = muhasebehesapDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MuhasebeHesapRead, MuhasebeHesapLtd")]
        public List<MuhasebeHesap> GetList()
        {
            return _muhasebehesapDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, MuhasebeHesapRead, MuhasebeHesapLtd")]
        public MuhasebeHesap GetById(int Id)
        {
            return _muhasebehesapDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MuhasebeHesapCreate")]
        public int Add(MuhasebeHesap muhasebehesap)
        {
            return _muhasebehesapDal.Add(muhasebehesap);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MuhasebeHesapUpdate")]
        public int Update(MuhasebeHesap muhasebehesap)
        {
            return _muhasebehesapDal.Update(muhasebehesap);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MuhasebeHesapDelete")]
        public int Delete(int Id)
        {
            return _muhasebehesapDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MuhasebeHesapDelete")]
        public int DeleteSoft(int Id)
        {
            return _muhasebehesapDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, MuhasebeHesapRead, MuhasebeHesapLtd")]
        public List<MuhasebeHesap> GetListPagination(PagingParams pagingParams)
        {
            return _muhasebehesapDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _muhasebehesapDal.GetCount(filter);
        }

    }
}
