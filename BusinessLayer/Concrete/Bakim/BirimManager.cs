using System.Collections.Generic;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Concrete.Bakim
{
    public class BirimManager : IBirimService
    {
        IBirimDal _birimDal;

        public BirimManager(IBirimDal birimDal)
        {
            _birimDal = birimDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BirimRead, BirimLtd")]
        public List<Birim> GetList()
        {
            return _birimDal.GetList();
        }
        [SecuredOperation(Roles = "Admin, BirimRead, BirimLtd")]
        public Birim GetById(int Id)
        {
            return _birimDal.Get(Id);
        }
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BirimCreate")]
        public int Add(Birim birim)
        {
            return _birimDal.Add(birim);
        }
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BirimUpdate")]
        public int Update(Birim birim)
        {
            return _birimDal.Update(birim);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BirimDelete")]
        public int Delete(int Id)
        {
            return _birimDal.Delete(Id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BirimDelete")]
        public int DeleteSoft(int Id)
        {
            return _birimDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BirimRead, BirimLtd")]
        public List<Birim> GetListPagination(PagingParams pagingParams)
        {
            return _birimDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _birimDal.GetCount(filter);
        }
    }
}