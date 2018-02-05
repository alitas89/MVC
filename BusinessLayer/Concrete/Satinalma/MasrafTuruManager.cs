using System.Collections.Generic;
using BusinessLayer.Abstract.Satinalma;
using DataAccessLayer.Abstract.Satinalma;
using Core.Aspects.Postsharp.CacheAspects;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using EntityLayer.Concrete.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;

namespace BusinessLayer.Concrete
{
    public class MasrafTuruManager : IMasrafTuruService
    {
        IMasrafTuruDal _masrafturuDal;

        public MasrafTuruManager(IMasrafTuruDal masrafturuDal)
        {
            _masrafturuDal = masrafturuDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<MasrafTuru> GetList()
        {
            return _masrafturuDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public MasrafTuru GetById(int Id)
        {
            return _masrafturuDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(MasrafTuru masrafturu)
        {
            return _masrafturuDal.Add(masrafturu);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(MasrafTuru masrafturu)
        {
            return _masrafturuDal.Update(masrafturu);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _masrafturuDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _masrafturuDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<MasrafTuru> GetListPagination(PagingParams pagingParams)
        {
            return _masrafturuDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filterCol = "", string filterVal = "")
        {
            return _masrafturuDal.GetCount(filterCol, filterVal);
        }

    }
}
