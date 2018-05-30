using System.Collections.Generic;
using BusinessLayer.Abstract.Malzeme;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace BusinessLayer.Concrete.Malzeme
{
    public class MalzemeSayimiManager : IMalzemeSayimiService
    {
        IMalzemeSayimiDal _malzemesayimiDal;

        public MalzemeSayimiManager(IMalzemeSayimiDal malzemesayimiDal)
        {
            _malzemesayimiDal = malzemesayimiDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemeSayimiRead, MalzemeSayimiLtd")]
        public List<MalzemeSayimi> GetList()
        {
            return _malzemesayimiDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemeSayimiRead, MalzemeSayimiLtd")]
        public MalzemeSayimi GetById(int Id)
        {
            return _malzemesayimiDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeCreate, MalzemeSayimiCreate")]
        public int Add(MalzemeSayimi malzemesayimi)
        {
            return _malzemesayimiDal.Add(malzemesayimi);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeUpdate, MalzemeSayimiUpdate")]
        public int Update(MalzemeSayimi malzemesayimi)
        {
            return _malzemesayimiDal.Update(malzemesayimi);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeDelete, MalzemeSayimiDelete")]
        public int Delete(int Id)
        {
            return _malzemesayimiDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, MalzemeDelete, MalzemeSayimiDelete")]
        public int DeleteSoft(int Id)
        {
            return _malzemesayimiDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemeSayimiRead, MalzemeSayimiLtd")]
        public List<MalzemeSayimi> GetListPagination(PagingParams pagingParams)
        {
            return _malzemesayimiDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _malzemesayimiDal.GetCount(filter);
        }

    }
}