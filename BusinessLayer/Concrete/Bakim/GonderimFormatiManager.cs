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
    public class GonderimFormatiManager : IGonderimFormatiService
    {
        IGonderimFormatiDal _gonderimformatiDal;

        public GonderimFormatiManager(IGonderimFormatiDal gonderimformatiDal)
        {
            _gonderimformatiDal = gonderimformatiDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, GonderimFormatiRead, GonderimFormatiLtd")]
        public List<GonderimFormati> GetList()
        {
            return _gonderimformatiDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, BakimRead, GonderimFormatiRead, GonderimFormatiLtd")]
        public GonderimFormati GetById(int Id)
        {
            return _gonderimformatiDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, GonderimFormatiCreate")]
        public int Add(GonderimFormati gonderimformati)
        {
            return _gonderimformatiDal.Add(gonderimformati);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, GonderimFormatiUpdate")]
        public int Update(GonderimFormati gonderimformati)
        {
            return _gonderimformatiDal.Update(gonderimformati);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, GonderimFormatiDelete")]
        public int Delete(int Id)
        {
            return _gonderimformatiDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, GonderimFormatiDelete")]
        public int DeleteSoft(int Id)
        {
            return _gonderimformatiDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, GonderimFormatiRead, GonderimFormatiLtd")]
        public List<GonderimFormati> GetListPagination(PagingParams pagingParams)
        {
            return _gonderimformatiDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _gonderimformatiDal.GetCount(filter);
        }
    }
}