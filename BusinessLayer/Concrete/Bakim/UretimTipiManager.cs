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
    public class UretimTipiManager : IUretimTipiService
    {
        IUretimTipiDal _uretimtipiDal;

        public UretimTipiManager(IUretimTipiDal uretimtipiDal)
        {
            _uretimtipiDal = uretimtipiDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, UretimTipiRead, UretimTipiLtd")]
        public List<UretimTipi> GetList()
        {
            return _uretimtipiDal.GetList();
        }
        [SecuredOperation(Roles = "Admin, BakimRead, UretimTipiRead, UretimTipiLtd")]
        public UretimTipi GetById(int Id)
        {
            return _uretimtipiDal.Get(Id);
        }
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, UretimTipiCreate")]
        public int Add(UretimTipi uretimtipi)
        {
            return _uretimtipiDal.Add(uretimtipi);
        }
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, UretimTipiUpdate")]
        public int Update(UretimTipi uretimtipi)
        {
            return _uretimtipiDal.Update(uretimtipi);
        }
        
        [SecuredOperation(Roles = "Admin, BakimDelete, UretimTipiDelete")]
        public int Delete(int Id)
        {
            return _uretimtipiDal.Delete(Id);
        }
        
        [SecuredOperation(Roles = "Admin, BakimDelete, UretimTipiDelete")]
        public int DeleteSoft(int Id)
        {
            return _uretimtipiDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, UretimTipiRead, UretimTipiLtd")]
        public List<UretimTipi> GetListPagination(PagingParams pagingParams)
        {
            return _uretimtipiDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _uretimtipiDal.GetCount(filter);
        }
    }
}