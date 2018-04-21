using BusinessLayer.Abstract.Malzeme;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.DtoModel.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;
using System.Collections.Generic;

namespace BusinessLayer.Concrete.Malzeme
{
    public class MalzemeHareketDetayManager : IMalzemeHareketDetayService
    {
        IMalzemeHareketDetayDal _malzemehareketdetayDal;

        public MalzemeHareketDetayManager(IMalzemeHareketDetayDal malzemehareketdetayDal)
        {
            _malzemehareketdetayDal = malzemehareketdetayDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemeHareketDetayRead, MalzemeHareketDetayLtd")]
        public List<MalzemeHareketDetay> GetList()
        {
            return _malzemehareketdetayDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemeHareketDetayRead, MalzemeHareketDetayLtd")]
        public MalzemeHareketDetay GetById(int Id)
        {
            return _malzemehareketdetayDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeCreate, MalzemeHareketDetayCreate")]
        public int Add(MalzemeHareketDetay malzemehareketdetay)
        {
            return _malzemehareketdetayDal.Add(malzemehareketdetay);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeUpdate, MalzemeHareketDetayUpdate")]
        public int Update(MalzemeHareketDetay malzemehareketdetay)
        {
            return _malzemehareketdetayDal.Update(malzemehareketdetay);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeDelete, MalzemeHareketDetayDelete")]
        public int Delete(int Id)
        {
            return _malzemehareketdetayDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeDelete, MalzemeHareketDetayDelete")]
        public int DeleteSoft(int Id)
        {
            return _malzemehareketdetayDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemeHareketDetayRead, MalzemeHareketDetayLtd")]
        public List<MalzemeHareketDetay> GetListPagination(PagingParams pagingParams)
        {
            return _malzemehareketdetayDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _malzemehareketdetayDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemeHareketDetayRead, MalzemeHareketDetayLtd")]
        public List<MalzemeHareketDetayDto> GetListByFisNo(int MalzemeHareketFisNo)
        {
            return _malzemehareketdetayDal.GetListByFisNo(MalzemeHareketFisNo);
        }
    }
}
