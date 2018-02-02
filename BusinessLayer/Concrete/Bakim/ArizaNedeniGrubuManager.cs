using System.Collections.Generic;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class ArizaNedeniGrubuManager : IArizaNedeniGrubuService
    {
        IArizaNedeniGrubuDal _arizanedenigrubuDal;

        public ArizaNedeniGrubuManager(IArizaNedeniGrubuDal arizanedenigrubuDal)
        {
            _arizanedenigrubuDal = arizanedenigrubuDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<ArizaNedeniGrubu> GetList()
        {
            return _arizanedenigrubuDal.GetList();
        }
        [SecuredOperation(Roles = "Admin,Editor")]
        public ArizaNedeniGrubu GetById(int Id)
        {
            return _arizanedenigrubuDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(ArizaNedeniGrubu arizanedenigrubu)
        {
            return _arizanedenigrubuDal.Add(arizanedenigrubu);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(ArizaNedeniGrubu arizanedenigrubu)
        {
            return _arizanedenigrubuDal.Update(arizanedenigrubu);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _arizanedenigrubuDal.Delete(Id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _arizanedenigrubuDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<ArizaNedeniGrubu> GetListPagination(PagingParams pagingParams)
        {
            return _arizanedenigrubuDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filterCol = "", string filterVal = "")
        {
            return _arizanedenigrubuDal.GetCount(filterCol, filterVal);
        }


    }
}