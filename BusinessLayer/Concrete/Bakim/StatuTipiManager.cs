using System.Collections.Generic;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class StatuTipiManager : IStatuTipiService
    {
        IStatuTipiDal _statutipiDal;

        public StatuTipiManager(IStatuTipiDal statutipiDal)
        {
            _statutipiDal = statutipiDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, StatuTipiRead, StatuTipiLtd")]
        public List<StatuTipi> GetList()
        {
            return _statutipiDal.GetList();
        }
        [SecuredOperation(Roles = "Admin, BakimRead, StatuTipiRead, StatuTipiLtd")]
        public StatuTipi GetById(int Id)
        {
            return _statutipiDal.Get(Id);
        }
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, StatuTipiCreate")]
        public int Add(StatuTipi statutipi)
        {
            return _statutipiDal.Add(statutipi);
        }
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, StatuTipiUpdate")]
        public int Update(StatuTipi statutipi)
        {
            return _statutipiDal.Update(statutipi);
        }
        
        [SecuredOperation(Roles = "Admin, BakimDelete, StatuTipiDelete")]
        public int Delete(int Id)
        {
            return _statutipiDal.Delete(Id);
        }
        
        [SecuredOperation(Roles = "Admin, BakimDelete, StatuTipiDelete")]
        public int DeleteSoft(int Id)
        {
            return _statutipiDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimDelete, StatuTipiRead, StatuTipiLtd")]
        public List<StatuTipi> GetListPagination(PagingParams pagingParams)
        {
            return _statutipiDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _statutipiDal.GetCount(filter);
        }
    }
}