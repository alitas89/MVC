using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;

namespace BusinessLayer.Concrete.Bakim
{
    public class IsEmriTuruManager : IIsEmriTuruService
    {
        IIsEmriTuruDal _isEmriTuruDal;

        public IsEmriTuruManager(IIsEmriTuruDal isEmriTuruDal)
        {
            _isEmriTuruDal = isEmriTuruDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, IsEmriTuruRead, IsEmriTuruLtd")]
        public List<IsEmriTuru> GetList()
        {
            return _isEmriTuruDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, BakimRead, IsEmriTuruRead, IsEmriTuruLtd")]
        public IsEmriTuru GetById(int Id)
        {
            return _isEmriTuruDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, IsEmriTuruCreate")]
        public int Add(IsEmriTuru ısemrituru)
        {
            return _isEmriTuruDal.Add(ısemrituru);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, IsEmriTuruUpdate")]
        public int Update(IsEmriTuru isEmriTuru)
        {
            return _isEmriTuruDal.Update(isEmriTuru);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, IsEmriTuruDelete")]
        public int Delete(int Id)
        {
            return _isEmriTuruDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, IsEmriTuruDelete")]
        public int DeleteSoft(int Id)
        {
            return _isEmriTuruDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, IsEmriTuruRead, IsEmriTuruLtd")]
        public List<IsEmriTuru> GetListPagination(PagingParams pagingParams)
        {
            return _isEmriTuruDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _isEmriTuruDal.GetCount(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<IsEmriTuru> listIsEmriTuru)
        {
            return _isEmriTuruDal.AddListWithTransactionBySablon(listIsEmriTuru);
        }

    }
}
