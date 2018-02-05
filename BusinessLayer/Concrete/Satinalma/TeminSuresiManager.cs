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
    public class TeminSuresiManager : ITeminSuresiService
    {
        ITeminSuresiDal _teminsuresiDal;

        public TeminSuresiManager(ITeminSuresiDal teminsuresiDal)
        {
            _teminsuresiDal = teminsuresiDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<TeminSuresi> GetList()
        {
            return _teminsuresiDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public TeminSuresi GetById(int Id)
        {
            return _teminsuresiDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(TeminSuresi teminsuresi)
        {
            return _teminsuresiDal.Add(teminsuresi);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(TeminSuresi teminsuresi)
        {
            return _teminsuresiDal.Update(teminsuresi);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _teminsuresiDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _teminsuresiDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<TeminSuresi> GetListPagination(PagingParams pagingParams)
        {
            return _teminsuresiDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filterCol = "", string filterVal = "")
        {
            return _teminsuresiDal.GetCount(filterCol, filterVal);
        }

    }
}
