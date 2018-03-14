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
    public class KaynakTuruManager : IKaynakTuruService
    {
        IKaynakTuruDal _kaynakturuDal;

        public KaynakTuruManager(IKaynakTuruDal kaynakturuDal)
        {
            _kaynakturuDal = kaynakturuDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<KaynakTuru> GetList()
        {
            return _kaynakturuDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public KaynakTuru GetById(int Id)
        {
            return _kaynakturuDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(KaynakTuru kaynakturu)
        {
            return _kaynakturuDal.Add(kaynakturu);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(KaynakTuru kaynakturu)
        {
            return _kaynakturuDal.Update(kaynakturu);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _kaynakturuDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _kaynakturuDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<KaynakTuru> GetListPagination(PagingParams pagingParams)
        {
            return _kaynakturuDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _kaynakturuDal.GetCount(filter);
        }

    }
}