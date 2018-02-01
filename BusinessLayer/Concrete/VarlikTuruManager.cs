using System.Collections.Generic;
using BusinessLayer.Abstract;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class VarlikTuruManager : IVarlikTuruService
    {
        IVarlikTuruDal _varlikturuDal;

        public VarlikTuruManager(IVarlikTuruDal varlikturuDal)
        {
            _varlikturuDal = varlikturuDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<VarlikTuru> GetList()
        {
            return _varlikturuDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public VarlikTuru GetById(int Id)
        {
            return _varlikturuDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(VarlikTuru varlikturu)
        {
            return _varlikturuDal.Add(varlikturu);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(VarlikTuru varlikturu)
        {
            return _varlikturuDal.Update(varlikturu);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _varlikturuDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _varlikturuDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<VarlikTuru> GetListPagination(PagingParams pagingParams)
        {
            return _varlikturuDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filterCol = "", string filterVal = "")
        {
            return _varlikturuDal.GetCount(filterCol, filterVal);
        }
    }
}