using BusinessLayer.Abstract.Malzeme;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;
using System.Collections.Generic;

namespace BusinessLayer.Concrete.Malzeme
{
    public class MalzemeHareketFisManager : IMalzemeHareketFisService
    {
        IMalzemeHareketFisDal _malzemehareketfisDal;

        public MalzemeHareketFisManager(IMalzemeHareketFisDal malzemehareketfisDal)
        {
            _malzemehareketfisDal = malzemehareketfisDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemeHareketFisRead, MalzemeHareketFisLtd")]
        public List<MalzemeHareketFis> GetList() => _malzemehareketfisDal.GetList();

        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemeHareketFisRead, MalzemeHareketFisLtd")]
        public MalzemeHareketFis GetById(int Id)
        {
            return _malzemehareketfisDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeCreate, MalzemeHareketFisCreate")]
        public int Add(MalzemeHareketFis malzemehareketfis)
        {
            return _malzemehareketfisDal.Add(malzemehareketfis);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeUpdate, MalzemeHareketFisUpdate")]
        public int Update(MalzemeHareketFis malzemehareketfis)
        {
            return _malzemehareketfisDal.Update(malzemehareketfis);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeDelete, MalzemeHareketFisDelete")]
        public int Delete(int Id)
        {
            return _malzemehareketfisDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeDelete, MalzemeHareketFisDelete")]
        public int DeleteSoft(int Id)
        {
            return _malzemehareketfisDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemeHareketFisRead, MalzemeHareketFisLtd")]
        public List<MalzemeHareketFis> GetListPagination(PagingParams pagingParams)
        {
            return _malzemehareketfisDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _malzemehareketfisDal.GetCount(filter);
        }
    }
}
