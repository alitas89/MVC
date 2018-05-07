using System.Collections.Generic;
using BusinessLayer.Abstract.Varlik;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Concrete.Varlik
{
    public class VarlikTuruManager : IVarlikTuruService
    {
        IVarlikTuruDal _varlikturuDal;

        public VarlikTuruManager(IVarlikTuruDal varlikturuDal)
        {
            _varlikturuDal = varlikturuDal;
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikTuruRead, VarlikTuruLtd")]
        public List<VarlikTuru> GetList()
        {
            return _varlikturuDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikTuruRead, VarlikTuruLtd")]
        public VarlikTuru GetById(int Id)
        {
            return _varlikturuDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikCreate, VarlikTuruCreate")]
        public int Add(VarlikTuru varlikturu)
        {
            return _varlikturuDal.Add(varlikturu);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikUpdate, VarlikTuruUpdate")]
        public int Update(VarlikTuru varlikturu)
        {
            return _varlikturuDal.Update(varlikturu);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, VarlikTuruDelete")]
        public int Delete(int Id)
        {
            return _varlikturuDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, VarlikTuruDelete")]
        public int DeleteSoft(int Id)
        {
            return _varlikturuDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikTuruRead, VarlikTuruLtd")]
        public List<VarlikTuru> GetListPagination(PagingParams pagingParams)
        {
            return _varlikturuDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _varlikturuDal.GetCount(filter);
        }
    }
}