using System.Collections.Generic;
using BusinessLayer.Abstract.Varlik;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Concrete.Varlik
{
    public class YasalTakipManager : IYasalTakipService
    {
        IYasalTakipDal _yasaltakipDal;

        public YasalTakipManager(IYasalTakipDal yasaltakipDal)
        {
            _yasaltakipDal = yasaltakipDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikRead, VarliklarRead, YasalTakipRead, YasalTakipLtd")]
        public List<YasalTakip> GetList()
        {
            return _yasaltakipDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarliklarRead, YasalTakipRead, YasalTakipLtd")]
        public YasalTakip GetById(int Id)
        {
            return _yasaltakipDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikCreate, VarliklarCreate, YasalTakipCreate")]
        public int Add(YasalTakip yasaltakip)
        {
            return _yasaltakipDal.Add(yasaltakip);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikUpdate, VarliklarUpdate, YasalTakipUpdate")]
        public int Update(YasalTakip yasaltakip)
        {
            return _yasaltakipDal.Update(yasaltakip);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikDelete, VarliklarDelete, YasalTakipDelete")]
        public int Delete(int Id)
        {
            return _yasaltakipDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikDelete, VarliklarRead, YasalTakipDelete")]
        public int DeleteSoft(int Id)
        {
            return _yasaltakipDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarliklarRead, YasalTakipRead, YasalTakipLtd")]
        public List<YasalTakip> GetListPagination(PagingParams pagingParams)
        {
            return _yasaltakipDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _yasaltakipDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarliklarRead, YasalTakipRead, YasalTakipLtd")]
        public YasalTakip GetYasalTakipByVarlikID(int VarlikID)
        {
            return _yasaltakipDal.GetYasalTakipByVarlikID(VarlikID);
        }
    }

}