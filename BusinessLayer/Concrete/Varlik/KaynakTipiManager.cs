using System.Collections.Generic;
using BusinessLayer.Abstract.Varlik;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace BusinessLayer.Concrete.Varlik
{
    public class KaynakTipiManager : IKaynakTipiService
    {
        IKaynakTipiDal _kaynaktipiDal;

        public KaynakTipiManager(IKaynakTipiDal kaynaktipiDal)
        {
            _kaynaktipiDal = kaynaktipiDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<KaynakTipi> GetList()
        {
            return _kaynaktipiDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public KaynakTipi GetById(int Id)
        {
            return _kaynaktipiDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(KaynakTipi kaynaktipi)
        {
            return _kaynaktipiDal.Add(kaynaktipi);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(KaynakTipi kaynaktipi)
        {
            return _kaynaktipiDal.Update(kaynaktipi);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _kaynaktipiDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _kaynaktipiDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<KaynakTipi> GetListPagination(PagingParams pagingParams)
        {
            return _kaynaktipiDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filterCol = "", string filterVal = "")
        {
            return _kaynaktipiDal.GetCount(filterCol, filterVal);
        }

    }
}