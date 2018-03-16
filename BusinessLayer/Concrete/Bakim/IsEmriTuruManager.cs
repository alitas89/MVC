using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;

namespace BusinessLayer.Concrete.Bakim
{
    public class IsEmriTuruManager : IIsEmriTuruService
    {
        IIsEmriTuruDal _isEmriTuruDal;

        public IsEmriTuruManager(IIsEmriTuruDal isEmriTuruDal)
        {
            _isEmriTuruDal = isEmriTuruDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IsEmriTuruRead, IsEmriTuruLtd")]
        public List<IsEmriTuru> GetList()
        {
            return _isEmriTuruDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, IsEmriTuruRead, IsEmriTuruLtd")]
        public IsEmriTuru GetById(int Id)
        {
            return _isEmriTuruDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IsEmriTuruCreate")]
        public int Add(IsEmriTuru ısemrituru)
        {
            return _isEmriTuruDal.Add(ısemrituru);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IsEmriTuruUpdate")]
        public int Update(IsEmriTuru isEmriTuru)
        {
            return _isEmriTuruDal.Update(isEmriTuru);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IsEmriTuruDelete")]
        public int Delete(int Id)
        {
            return _isEmriTuruDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IsEmriTuruDelete")]
        public int DeleteSoft(int Id)
        {
            return _isEmriTuruDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, IsEmriTuruRead, IsEmriTuruLtd")]
        public List<IsEmriTuru> GetListPagination(PagingParams pagingParams)
        {
            return _isEmriTuruDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _isEmriTuruDal.GetCount(filter);
        }

    }
}
