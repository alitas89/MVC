using BusinessLayer.Abstract.Malzeme;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;
using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Malzeme;
using Newtonsoft.Json;

namespace BusinessLayer.Concrete.Malzeme
{
    public class MalzemeHareketManager : IMalzemeHareketService
    {
        IMalzemeHareketDal _malzemehareketDal;

        public MalzemeHareketManager(IMalzemeHareketDal malzemehareketDal)
        {
            _malzemehareketDal = malzemehareketDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeHareketRead, MalzemeHareketLtd")]
        public List<MalzemeHareket> GetList()
        {
            return _malzemehareketDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, MalzemeHareketRead, MalzemeHareketLtd")]
        public MalzemeHareket GetById(int Id)
        {
            return _malzemehareketDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeHareketCreate")]
        public int Add(MalzemeHareket malzemehareket)
        {
            return _malzemehareketDal.Add(malzemehareket);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeHareketUpdate")]
        public int Update(MalzemeHareket malzemehareket)
        {
            return _malzemehareketDal.Update(malzemehareket);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeHareketDelete")]
        public int Delete(int Id)
        {
            return _malzemehareketDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeHareketDelete")]
        public int DeleteSoft(int Id)
        {
            return _malzemehareketDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, MalzemeHareketRead, MalzemeHareketLtd")]
        public List<MalzemeHareket> GetListPagination(PagingParams pagingParams)
        {
            return _malzemehareketDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _malzemehareketDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, MalzemeHareketRead")]
        public List<MalzemeHareketDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _malzemehareketDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _malzemehareketDal.GetCountDto(filter);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeHareketCreate")]
        public int AddMalzemeHareket(MalzemeHareketTemp malzemeHareketTemp)
        {
            var listMalzeme = JsonConvert.DeserializeObject<List<MalzemeHareketDetay>>(malzemeHareketTemp.arrMalzeme);

            var count = _malzemehareketDal.AddWithTransaction(malzemeHareketTemp, listMalzeme);

            return count;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeHareketUpdate")]
        public int UpdateMalzemeHareket(MalzemeHareketTemp malzemeHareketTemp)
        {
            var listMalzeme = JsonConvert.DeserializeObject<List<MalzemeHareketDetay>>(malzemeHareketTemp.arrMalzeme);

            var count = _malzemehareketDal.UpdateWithTransaction(malzemeHareketTemp, listMalzeme);

            return count;
        }
    }
}
