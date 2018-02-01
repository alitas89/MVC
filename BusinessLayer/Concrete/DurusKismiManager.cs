using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules.FluentValidation;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.Aspects.Postsharp.ValidationAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class DurusKismiManager : IDurusKismiService
    {
        IDurusKismiDal _duruskismiDal;

        public DurusKismiManager(IDurusKismiDal duruskismiDal)
        {
            _duruskismiDal = duruskismiDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<DurusKismi> GetList()
        {
            return _duruskismiDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public DurusKismi GetById(int Id)
        {
            return _duruskismiDal.Get(Id);
        }


        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [FluentValidationAspect(typeof(DurusKismiValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(DurusKismi duruskismi)
        {
            return _duruskismiDal.Add(duruskismi);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [FluentValidationAspect(typeof(DurusKismiValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(DurusKismi duruskismi)
        {
            return _duruskismiDal.Update(duruskismi);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _duruskismiDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _duruskismiDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<DurusKismi> GetListPagination(PagingParams pagingParams)
        {
            return _duruskismiDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filterCol = "", string filterVal = "")
        {
            return _duruskismiDal.GetCount(filterCol, filterVal);
        }
    }
}
