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
    public class MalzemeAltGrupManager : IMalzemeAltGrupService
    {
        IMalzemeAltGrupDal _malzemealtgrupDal;

        public MalzemeAltGrupManager(IMalzemeAltGrupDal malzemealtgrupDal)
        {
            _malzemealtgrupDal = malzemealtgrupDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<MalzemeAltGrup> GetList()
        {
            return _malzemealtgrupDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public MalzemeAltGrup GetById(int Id)
        {
            return _malzemealtgrupDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(MalzemeAltGrup malzemealtgrup)
        {
            return _malzemealtgrupDal.Add(malzemealtgrup);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(MalzemeAltGrup malzemealtgrup)
        {
            return _malzemealtgrupDal.Update(malzemealtgrup);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _malzemealtgrupDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _malzemealtgrupDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<MalzemeAltGrup> GetListPagination(PagingParams pagingParams)
        {
            return _malzemealtgrupDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filterCol = "", string filterVal = "")
        {
            return _malzemealtgrupDal.GetCount(filterCol, filterVal);
        }

    }
}
