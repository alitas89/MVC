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
    public class EgitimManager : IEgitimService
    {
        IEgitimDal _egitimDal;

        public EgitimManager(IEgitimDal egitimDal)
        {
            _egitimDal = egitimDal;
        }

        
        [SecuredOperation(Roles = "Admin, PersonelRead, EgitimRead, EgitimLtd")]
        public List<Egitim> GetList()
        {
            return _egitimDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, PersonelRead, EgitimRead, EgitimLtd")]
        public Egitim GetById(int Id)
        {
            return _egitimDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, PersonelCreate, EgitimCreate")]
        public int Add(Egitim egitim)
        {
            return _egitimDal.Add(egitim);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, PersonelUpdate, EgitimUpdate")]
        public int Update(Egitim egitim)
        {
            return _egitimDal.Update(egitim);
        }

        
        [SecuredOperation(Roles = "Admin, PersonelDelete, EgitimDelete")]
        public int Delete(int Id)
        {
            return _egitimDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, PersonelDelete, EgitimDelete")]
        public int DeleteSoft(int Id)
        {
            return _egitimDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, PersonelRead, EgitimRead, EgitimLtd")]
        public List<Egitim> GetListPagination(PagingParams pagingParams)
        {
            return _egitimDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _egitimDal.GetCount(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<Egitim> listEgitim)
        {
            return _egitimDal.AddListWithTransactionBySablon(listEgitim);
        }

    }
}
