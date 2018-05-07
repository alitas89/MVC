using System.Collections.Generic;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class BilgilendirmeGrubuManager : IBilgilendirmeGrubuService
    {
        IBilgilendirmeGrubuDal _bilgilendirmegrubuDal;

        public BilgilendirmeGrubuManager(IBilgilendirmeGrubuDal bilgilendirmegrubuDal)
        {
            _bilgilendirmegrubuDal = bilgilendirmegrubuDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, BilgilendirmeGrubuRead, BilgilendirmeGrubuLtd")]
        public List<BilgilendirmeGrubu> GetList()
        {
            return _bilgilendirmegrubuDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, BakimRead, BilgilendirmeGrubuRead, BilgilendirmeGrubuLtd")]
        public BilgilendirmeGrubu GetById(int Id)
        {
            return _bilgilendirmegrubuDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, BilgilendirmeGrubuCreate")]
        public int Add(BilgilendirmeGrubu bilgilendirmegrubu)
        {
            return _bilgilendirmegrubuDal.Add(bilgilendirmegrubu);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, BilgilendirmeGrubuUpdate")]
        public int Update(BilgilendirmeGrubu bilgilendirmegrubu)
        {
            return _bilgilendirmegrubuDal.Update(bilgilendirmegrubu);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, BilgilendirmeGrubuDelete")]
        public int Delete(int Id)
        {
            return _bilgilendirmegrubuDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, BilgilendirmeGrubuDelete")]
        public int DeleteSoft(int Id)
        {
            return _bilgilendirmegrubuDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, BilgilendirmeGrubuRead, BilgilendirmeGrubuLtd")]
        public List<BilgilendirmeGrubu> GetListPagination(PagingParams pagingParams)
        {
            return _bilgilendirmegrubuDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _bilgilendirmegrubuDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, BilgilendirmeGrubuRead, BilgilendirmeGrubuLtd")]
        public List<BilgilendirmeGrubuDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _bilgilendirmegrubuDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _bilgilendirmegrubuDal.GetCountDto(filter);
        }
    }
}