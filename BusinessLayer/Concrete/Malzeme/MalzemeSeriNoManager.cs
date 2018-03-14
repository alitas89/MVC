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
    public class MalzemeSeriNoManager : IMalzemeSeriNoService
    {
        IMalzemeSeriNoDal _malzemeserinoDal;

        public MalzemeSeriNoManager(IMalzemeSeriNoDal malzemeserinoDal)
        {
            _malzemeserinoDal = malzemeserinoDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<MalzemeSeriNo> GetList()
        {
            return _malzemeserinoDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public MalzemeSeriNo GetById(int Id)
        {
            return _malzemeserinoDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(MalzemeSeriNo malzemeserino)
        {
            return _malzemeserinoDal.Add(malzemeserino);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(MalzemeSeriNo malzemeserino)
        {
            return _malzemeserinoDal.Update(malzemeserino);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _malzemeserinoDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _malzemeserinoDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<MalzemeSeriNo> GetListPagination(PagingParams pagingParams)
        {
            return _malzemeserinoDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _malzemeserinoDal.GetCount(filter);
        }

    }
}
