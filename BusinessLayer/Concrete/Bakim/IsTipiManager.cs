using System.Collections.Generic;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class IsTipiManager : IIsTipiService
    {
        IIsTipiDal _isTipiDal;

        public IsTipiManager(IIsTipiDal isTipiDal)
        {
            _isTipiDal = isTipiDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, IsTipiRead, IsTipiLtd")]
        public List<IsTipi> GetList()
        {
            return _isTipiDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, BakimRead, IsTipiRead, IsTipiLtd")]
        public IsTipi GetById(int Id)
        {
            return _isTipiDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, IsTipiCreate")]
        public int Add(IsTipi isTipi)
        {
            return _isTipiDal.Add(isTipi);
        }
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, IsTipiUpdate")]
        public int Update(IsTipi isTipi)
        {
            return _isTipiDal.Update(isTipi);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, IsTipiDelete")]
        public int Delete(int Id)
        {
            return _isTipiDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, IsTipiDelete")]
        public int DeleteSoft(int Id)
        {
            return _isTipiDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, IsTipiRead, IsTipiLtd")]
        public List<IsTipi> GetListPagination(PagingParams pagingParams)
        {
            return _isTipiDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _isTipiDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, IsTipiRead, IsTipiLtd")]
        public List<IsTipiDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _isTipiDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _isTipiDal.GetCountDto(filter);
        }        
    }
}
