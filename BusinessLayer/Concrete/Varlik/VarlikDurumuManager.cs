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
    public class VarlikDurumuManager : IVarlikDurumuService
    {
        IVarlikDurumuDal _varlikdurumuDal;

        public VarlikDurumuManager(IVarlikDurumuDal varlikdurumuDal)
        {
            _varlikdurumuDal = varlikdurumuDal;
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikDurumuRead, VarlikDurumuLtd")]
        public List<VarlikDurumu> GetList()
        {
            return _varlikdurumuDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikDurumuRead, VarlikDurumuLtd")]
        public VarlikDurumu GetById(int Id)
        {
            return _varlikdurumuDal.Get(Id);
        }

        
        //[FluentValidationAspect(typeof(DurusNedeniValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin, VarlikCreate, VarlikDurumuCreate")]
        public int Add(VarlikDurumu varlikdurumu)
        {
            return _varlikdurumuDal.Add(varlikdurumu);
        }

        
        //[FluentValidationAspect(typeof(DurusNedeniValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin, VarlikUpdate, VarlikDurumuUpdate")]
        public int Update(VarlikDurumu varlikdurumu)
        {
            return _varlikdurumuDal.Update(varlikdurumu);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, VarlikDurumuDelete")]
        public int Delete(int Id)
        {
            return _varlikdurumuDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, VarlikDurumuDelete")]
        public int DeleteSoft(int Id)
        {
            return _varlikdurumuDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikDurumuRead, VarlikDurumuLtd")]
        public List<VarlikDurumu> GetListPagination(PagingParams pagingParams)
        {
            return _varlikdurumuDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _varlikdurumuDal.GetCount(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<VarlikDurumu> listVarlikDurumu)
        {
            return _varlikdurumuDal.AddListWithTransactionBySablon(listVarlikDurumu);
        }
    }
}
