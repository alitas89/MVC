using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract.Personel;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace BusinessLayer.Concrete.Personel
{
    public class KaynakSinifiManager : IKaynakSinifiService
    {
        IKaynakSinifiDal _kaynaksinifiDal;

        public KaynakSinifiManager(IKaynakSinifiDal kaynaksinifiDal)
        {
            _kaynaksinifiDal = kaynaksinifiDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<KaynakSinifi> GetList()
        {
            return _kaynaksinifiDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public KaynakSinifi GetById(int Id)
        {
            return _kaynaksinifiDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(KaynakSinifi kaynaksinifi)
        {
            return _kaynaksinifiDal.Add(kaynaksinifi);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(KaynakSinifi kaynaksinifi)
        {
            return _kaynaksinifiDal.Update(kaynaksinifi);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _kaynaksinifiDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _kaynaksinifiDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<KaynakSinifi> GetListPagination(PagingParams pagingParams)
        {
            return _kaynaksinifiDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _kaynaksinifiDal.GetCount(filter);
        }

    }
}
