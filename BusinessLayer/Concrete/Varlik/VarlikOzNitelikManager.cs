using BusinessLayer.Abstract.Varlik;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BusinessLayer.Concrete.Varlik
{
    public class VarlikOzNitelikManager : IVarlikOzNitelikService
    {
        IVarlikOzNitelikDal _varlikoznitelikDal;

        public VarlikOzNitelikManager(IVarlikOzNitelikDal varlikoznitelikDal)
        {
            _varlikoznitelikDal = varlikoznitelikDal;
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikOzNitelikRead, VarlikOzNitelikLtd")]
        public List<VarlikOzNitelik> GetList()
        {
            return _varlikoznitelikDal.GetList();
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikOzNitelikRead, VarlikOzNitelikLtd")]
        public List<VarlikOzNitelik> GetListByVarlikID(int VarlikID)
        {
            return _varlikoznitelikDal.GetListByVarlikID(VarlikID);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikOzNitelikRead, VarlikOzNitelikLtd")]
        public VarlikOzNitelik GetById(int Id)
        {
            return _varlikoznitelikDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikCreate, VarlikOzNitelikCreate")]
        public int Add(VarlikOzNitelik varlikoznitelik)
        {
            return _varlikoznitelikDal.Add(varlikoznitelik);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikUpdate, VarlikOzNitelikUpdate")]
        public int Update(VarlikOzNitelik varlikoznitelik)
        {
            return _varlikoznitelikDal.Update(varlikoznitelik);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, VarlikOzNitelikDelete")]
        public int Delete(int Id)
        {
            return _varlikoznitelikDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, VarlikOzNitelikDelete")]
        public int DeleteSoft(int Id)
        {
            return _varlikoznitelikDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikOzNitelikRead, VarlikOzNitelikLtd")]
        public List<VarlikOzNitelik> GetListPagination(PagingParams pagingParams)
        {
            return _varlikoznitelikDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _varlikoznitelikDal.GetCount(filter);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikCreate, VarlikOzNitelikCreate")]
        public int AddVarlikOzNitelik(int varlikID, string arrVarlikOzNitelik)
        {
            var listOzNitelik = JsonConvert.DeserializeObject<List<VarlikOzNitelik>>(arrVarlikOzNitelik);

            var count = _varlikoznitelikDal.AddWithTransaction(varlikID, listOzNitelik);

            return count;
        }

        
        [SecuredOperation(Roles = "Admin, VarlikUpdate, VarlikOzNitelikUpdate")]
        public int UpdateVarlikOzNitelik(int varlikID, string arrVarlikOzNitelik)
        {
            var listOzNitelik = JsonConvert.DeserializeObject<List<VarlikOzNitelikDto>>(arrVarlikOzNitelik);

            var count = _varlikoznitelikDal.UpdateWithTransaction(varlikID, listOzNitelik);

            return count;
        }       
    }
}
