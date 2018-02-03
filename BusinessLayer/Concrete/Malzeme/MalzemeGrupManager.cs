using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract.Malzeme;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace BusinessLayer.Concrete.Malzeme
{
    public class MalzemeGrupManager : IMalzemeGrupService
    {
        IMalzemeGrupDal _malzemegrupDal;

        public MalzemeGrupManager(IMalzemeGrupDal malzemegrupDal)
        {
            _malzemegrupDal = malzemegrupDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<MalzemeGrup> GetList()
        {
            return _malzemegrupDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public MalzemeGrup GetById(int Id)
        {
            return _malzemegrupDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(MalzemeGrup malzemegrup)
        {
            return _malzemegrupDal.Add(malzemegrup);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(MalzemeGrup malzemegrup)
        {
            return _malzemegrupDal.Update(malzemegrup);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _malzemegrupDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _malzemegrupDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<MalzemeGrup> GetListPagination(PagingParams pagingParams)
        {
            return _malzemegrupDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filterCol = "", string filterVal = "")
        {
            return _malzemegrupDal.GetCount(filterCol, filterVal);
        }

    }
}
