using System.Collections.Generic;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class BakimOncelikManager : IBakimOncelikService
    {
        IBakimOncelikDal _bakimoncelikDal;

        public BakimOncelikManager(IBakimOncelikDal bakimoncelikDal)
        {
            _bakimoncelikDal = bakimoncelikDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, BakimOncelikRead, BakimOncelikLtd")]
        public List<BakimOncelik> GetList()
        {
            return _bakimoncelikDal.GetList();
        }
        [SecuredOperation(Roles = "Admin, BakimRead, BakimOncelikRead, BakimOncelikLtd")]
        public BakimOncelik GetById(int Id)
        {
            return _bakimoncelikDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, BakimOncelikCreate")]
        public int Add(BakimOncelik bakimoncelik)
        {
            return _bakimoncelikDal.Add(bakimoncelik);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, BakimOncelikUpdate")]
        public int Update(BakimOncelik bakimoncelik)
        {
            return _bakimoncelikDal.Update(bakimoncelik);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, BakimOncelikDelete")]
        public int Delete(int Id)
        {
            return _bakimoncelikDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, BakimOncelikDelete")]
        public int DeleteSoft(int Id)
        {
            return _bakimoncelikDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, BakimOncelikRead, BakimOncelikLtd")]
        public List<BakimOncelik> GetListPagination(PagingParams pagingParams)
        {
            return _bakimoncelikDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _bakimoncelikDal.GetCount(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<BakimOncelik> listBakimOncelik)
        {
            return _bakimoncelikDal.AddListWithTransactionBySablon(listBakimOncelik);
        }
    }
}