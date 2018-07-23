using BusinessLayer.Abstract.Personel;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;
using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Personel;
using EntityLayer.ComplexTypes.DtoModel.Varlik;

namespace BusinessLayer.Concrete.Personel
{
    public class KaynakManager : IKaynakService
    {
        IKaynakDal _kaynakDal;

        public KaynakManager(IKaynakDal kaynakDal)
        {
            _kaynakDal = kaynakDal;
        }

        
        [SecuredOperation(Roles = "Admin, PersonelRead, KaynakRead, KaynakLtd")]
        public List<Kaynak> GetList()
        {
            return _kaynakDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, PersonelRead, KaynakRead, KaynakLtd")]
        public Kaynak GetById(int Id)
        {
            return _kaynakDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, PersonelCreate, KaynakCreate")]
        public int Add(Kaynak kaynak)
        {
            return _kaynakDal.Add(kaynak);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, PersonelUpdate, KaynakUpdate")]
        public int Update(Kaynak kaynak)
        {
            return _kaynakDal.Update(kaynak);
        }

        
        [SecuredOperation(Roles = "Admin, PersonelDelete, KaynakDelete")]
        public int Delete(int Id)
        {
            return _kaynakDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, PersonelDelete, KaynakDelete")]
        public int DeleteSoft(int Id)
        {
            return _kaynakDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, PersonelRead, KaynakRead, KaynakLtd")]
        public List<Kaynak> GetListPagination(PagingParams pagingParams)
        {
            return _kaynakDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _kaynakDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, PersonelRead, KaynakRead, KaynakLtd")]
        public List<KaynakDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _kaynakDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _kaynakDal.GetCountDto(filter);
        }

        public List<Kaynak> GetListKaynakHaveKullaniciID()
        {
            return _kaynakDal.GetListKaynakHaveKullaniciID();
        }

        public List<string> AddListWithTransactionBySablon(List<Kaynak> listKaynak)
        {
            return _kaynakDal.AddListWithTransactionBySablon(listKaynak);
        }
    }
}
