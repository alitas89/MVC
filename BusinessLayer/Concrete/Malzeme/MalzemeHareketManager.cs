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

        
        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemeHareketRead, MalzemeHareketLtd")]
        public List<MalzemeHareket> GetList()
        {
            return _malzemehareketDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemeHareketRead, MalzemeHareketLtd")]
        public MalzemeHareket GetById(int Id)
        {
            return _malzemehareketDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, MalzemeCreate, MalzemeHareketCreate")]
        public int Add(MalzemeHareket malzemehareket)
        {
            return _malzemehareketDal.Add(malzemehareket);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, MalzemeUpdate, MalzemeHareketUpdate")]
        public int Update(MalzemeHareket malzemehareket)
        {
            return _malzemehareketDal.Update(malzemehareket);
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeDelete, MalzemeHareketDelete")]
        public int Delete(int Id)
        {
            return _malzemehareketDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeDelete, MalzemeHareketDelete")]
        public int DeleteSoft(int Id)
        {
            return _malzemehareketDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemeHareketRead, MalzemeHareketLtd")]
        public List<MalzemeHareket> GetListPagination(PagingParams pagingParams)
        {
            return _malzemehareketDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _malzemehareketDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemeHareketRead")]
        public List<MalzemeHareketDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _malzemehareketDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _malzemehareketDal.GetCountDto(filter);
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeCreate, MalzemeHareketCreate")]
        public int AddMalzemeHareket(MalzemeHareketTemp malzemeHareketTemp)
        {
            var listMalzeme = JsonConvert.DeserializeObject<List<MalzemeHareketDetay>>(malzemeHareketTemp.arrMalzeme);

            var count = _malzemehareketDal.AddWithTransaction(malzemeHareketTemp, listMalzeme);

            return count;
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeUpdate, MalzemeHareketUpdate")]
        public int UpdateMalzemeHareket(MalzemeHareketTemp malzemeHareketTemp)
        {
            var listMalzeme = JsonConvert.DeserializeObject<List<MalzemeHareketDetay>>(malzemeHareketTemp.arrMalzeme);

            var count = _malzemehareketDal.UpdateWithTransaction(malzemeHareketTemp, listMalzeme);

            return count;
        }
    }
}
