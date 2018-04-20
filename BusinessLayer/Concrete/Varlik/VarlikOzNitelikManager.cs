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

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikOzNitelikRead, VarlikOzNitelikLtd")]
        public List<VarlikOzNitelik> GetList()
        {
            return _varlikoznitelikDal.GetList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikOzNitelikRead, VarlikOzNitelikLtd")]
        public List<VarlikOzNitelik> GetListByVarlikID(int VarlikID)
        {
            return _varlikoznitelikDal.GetListByVarlikID(VarlikID);
        }

        [SecuredOperation(Roles = "Admin, VarlikOzNitelikRead, VarlikOzNitelikLtd")]
        public VarlikOzNitelik GetById(int Id)
        {
            return _varlikoznitelikDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikOzNitelikCreate")]
        public int Add(VarlikOzNitelik varlikoznitelik)
        {
            return _varlikoznitelikDal.Add(varlikoznitelik);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikOzNitelikUpdate")]
        public int Update(VarlikOzNitelik varlikoznitelik)
        {
            return _varlikoznitelikDal.Update(varlikoznitelik);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikOzNitelikDelete")]
        public int Delete(int Id)
        {
            return _varlikoznitelikDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikOzNitelikDelete")]
        public int DeleteSoft(int Id)
        {
            return _varlikoznitelikDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikOzNitelikRead, VarlikOzNitelikLtd")]
        public List<VarlikOzNitelik> GetListPagination(PagingParams pagingParams)
        {
            return _varlikoznitelikDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _varlikoznitelikDal.GetCount(filter);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikOzNitelikCreate")]
        public int AddVarlikOzNitelik(int varlikID, string arrVarlikOzNitelik)
        {
            var listOzNitelik = JsonConvert.DeserializeObject<List<VarlikOzNitelik>>(arrVarlikOzNitelik);

            var count = _varlikoznitelikDal.AddWithTransaction(varlikID, listOzNitelik);

            return count;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikOzNitelikUpdate")]
        public int UpdateVarlikOzNitelik(int varlikID, string arrVarlikOzNitelik)
        {
            var listOzNitelik = JsonConvert.DeserializeObject<List<VarlikOzNitelikDto>>(arrVarlikOzNitelik);

            var count = _varlikoznitelikDal.UpdateWithTransaction(varlikID, listOzNitelik);

            return count;
        }       
    }
}
