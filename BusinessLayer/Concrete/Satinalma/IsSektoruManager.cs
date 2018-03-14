using System.Collections.Generic;
using BusinessLayer.Abstract.Satinalma;
using DataAccessLayer.Abstract.Satinalma;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Aspects.Postsharp.CacheAspects;
using Core.Aspects.Postsharp.AuthorizationAspects;
using EntityLayer.Concrete.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;

namespace BusinessLayer.Concrete.Satinalma
{
    public class IsSektoruManager : IIsSektoruService
    {
        IIsSektoruDal _issektoruDal;

        public IsSektoruManager(IIsSektoruDal ıssektoruDal)
        {
            _issektoruDal = ıssektoruDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<IsSektoru> GetList()
        {
            return _issektoruDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public IsSektoru GetById(int Id)
        {
            return _issektoruDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(IsSektoru ıssektoru)
        {
            return _issektoruDal.Add(ıssektoru);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(IsSektoru ıssektoru)
        {
            return _issektoruDal.Update(ıssektoru);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _issektoruDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _issektoruDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<IsSektoru> GetListPagination(PagingParams pagingParams)
        {
            return _issektoruDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _issektoruDal.GetCount(filter);
        }

    }
}
