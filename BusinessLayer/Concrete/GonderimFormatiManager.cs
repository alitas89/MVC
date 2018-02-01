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
    public class GonderimFormatiManager : IGonderimFormatiService
    {
        IGonderimFormatiDal _gonderimformatiDal;

        public GonderimFormatiManager(IGonderimFormatiDal gonderimformatiDal)
        {
            _gonderimformatiDal = gonderimformatiDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<GonderimFormati> GetList()
        {
            return _gonderimformatiDal.GetList();
        }
        [SecuredOperation(Roles = "Admin,Editor")]
        public GonderimFormati GetById(int Id)
        {
            return _gonderimformatiDal.Get(Id);
        }
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(GonderimFormati gonderimformati)
        {
            return _gonderimformatiDal.Add(gonderimformati);
        }
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(GonderimFormati gonderimformati)
        {
            return _gonderimformatiDal.Update(gonderimformati);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _gonderimformatiDal.Delete(Id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _gonderimformatiDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<GonderimFormati> GetListPagination(PagingParams pagingParams)
        {
            return _gonderimformatiDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filterCol = "", string filterVal = "")
        {
            return _gonderimformatiDal.GetCount(filterCol, filterVal);
        }
    }
}