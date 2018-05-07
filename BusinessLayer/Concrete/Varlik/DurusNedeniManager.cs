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
    public class DurusNedeniManager : IDurusNedeniService
    {
        IDurusNedeniDal _durusnedeniDal;

        public DurusNedeniManager(IDurusNedeniDal durusnedeniDal)
        {
            _durusnedeniDal = durusnedeniDal;
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, DurusNedeniRead, DurusNedeniLtd")]
        public List<DurusNedeni> GetList()
        {
            return _durusnedeniDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, DurusNedeniRead, DurusNedeniLtd")]
        public DurusNedeni GetById(int Id)
        {
            return _durusnedeniDal.Get(Id);
        }

        
        [FluentValidationAspect(typeof(DurusNedeniValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin, VarlikCreate, DurusNedeniCreate")]
        public int Add(DurusNedeni durusnedeni)
        {
            return _durusnedeniDal.Add(durusnedeni);
        }

        
        [FluentValidationAspect(typeof(DurusNedeniValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin, VarlikUpdate, DurusNedeniUpdate")]
        public int Update(DurusNedeni durusnedeni)
        {
            return _durusnedeniDal.Update(durusnedeni);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, DurusNedeniDelete")]
        public int Delete(int Id)
        {
            return _durusnedeniDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, DurusNedeniDelete")]
        public int DeleteSoft(int Id)
        {
            return _durusnedeniDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, DurusNedeniRead, DurusNedeniLtd")]
        public List<DurusNedeni> GetListPagination(PagingParams pagingParams)
        {
            return _durusnedeniDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _durusnedeniDal.GetCount(filter);
        }
    }
}
