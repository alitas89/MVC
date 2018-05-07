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
    public class ArizaCozumuManager : IArizaCozumuService
    {
        IArizaCozumuDal _arizacozumuDal;

        public ArizaCozumuManager(IArizaCozumuDal arizacozumuDal)
        {
            _arizacozumuDal = arizacozumuDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, ArizaCozumuRead, ArizaCozumuLtd")]
        public List<ArizaCozumu> GetList()
        {
            return _arizacozumuDal.GetList();
        }
        [SecuredOperation(Roles = "Admin, BakimRead, ArizaCozumuRead, ArizaCozumuLtd")]
        public ArizaCozumu GetById(int Id)
        {
            return _arizacozumuDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, ArizaCozumuCreate")]
        public int Add(ArizaCozumu arizacozumu)
        {
            return _arizacozumuDal.Add(arizacozumu);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, ArizaCozumuUpdate")]
        public int Update(ArizaCozumu arizacozumu)
        {
            return _arizacozumuDal.Update(arizacozumu);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, ArizaCozumuDelete")]
        public int Delete(int Id)
        {
            return _arizacozumuDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, ArizaCozumuDelete")]
        public int DeleteSoft(int Id)
        {
            return _arizacozumuDal.DeleteSoft(Id);
        }
        
        [SecuredOperation(Roles = "Admin, BakimRead,Editor,ArizaCozumuRead, ArizaCozumuLtd")]
        public List<ArizaCozumu> GetListPagination(PagingParams pagingParams)
        {
            return _arizacozumuDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _arizacozumuDal.GetCount(filter);
        }
    }
}