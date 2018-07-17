using System.Collections.Generic;
using BusinessLayer.Abstract.Varlik;
using BusinessLayer.ValidationRules.FluentValidation;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.Aspects.Postsharp.ValidationAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Concrete.Varlik
{
    public class DurusKismiManager : IDurusKismiService
    {
        IDurusKismiDal _duruskismiDal;

        public DurusKismiManager(IDurusKismiDal duruskismiDal)
        {
            _duruskismiDal = duruskismiDal;
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, DurusKismiRead, DurusKismiLtd")]
        public List<DurusKismi> GetList()
        {
            return _duruskismiDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, DurusKismiRead, DurusKismiLtd")]
        public DurusKismi GetById(int Id)
        {
            return _duruskismiDal.Get(Id);
        }


        
        [FluentValidationAspect(typeof(DurusKismiValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin, VarlikCreate, DurusKismiCreate")]
        public int Add(DurusKismi duruskismi)
        {
            return _duruskismiDal.Add(duruskismi);
        }

        
        [FluentValidationAspect(typeof(DurusKismiValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin, VarlikUpdate, DurusKismiUpdate")]
        public int Update(DurusKismi duruskismi)
        {
            return _duruskismiDal.Update(duruskismi);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, DurusKismiDelete")]
        public int Delete(int Id)
        {
            return _duruskismiDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, DurusKismiDelete")]
        public int DeleteSoft(int Id)
        {
            return _duruskismiDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, DurusKismiRead, DurusKismiLtd")]
        public List<DurusKismi> GetListPagination(PagingParams pagingParams)
        {
            return _duruskismiDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _duruskismiDal.GetCount(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<DurusKismi> listDurusKismi)
        {
            return _duruskismiDal.AddListWithTransactionBySablon(listDurusKismi);
        }

    }
}
