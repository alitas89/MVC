using System.Collections.Generic;
using BusinessLayer.Abstract;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class BeklemeIptalNedeniManager : IBeklemeIptalNedeniService
    {
        IBeklemeIptalNedeniDal _beklemeıptalnedeniDal;

        public BeklemeIptalNedeniManager(IBeklemeIptalNedeniDal beklemeıptalnedeniDal)
        {
            _beklemeıptalnedeniDal = beklemeıptalnedeniDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<BeklemeIptalNedeni> GetList()
        {
            return _beklemeıptalnedeniDal.GetList();
        }
        [SecuredOperation(Roles = "Admin,Editor")]
        public BeklemeIptalNedeni GetById(int Id)
        {
            return _beklemeıptalnedeniDal.Get(Id);
        }
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(BeklemeIptalNedeni beklemeıptalnedeni)
        {
            return _beklemeıptalnedeniDal.Add(beklemeıptalnedeni);
        }
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(BeklemeIptalNedeni beklemeıptalnedeni)
        {
            return _beklemeıptalnedeniDal.Update(beklemeıptalnedeni);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _beklemeıptalnedeniDal.Delete(Id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _beklemeıptalnedeniDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<BeklemeIptalNedeni> GetListPagination(PagingParams pagingParams)
        {
            return _beklemeıptalnedeniDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filterCol = "", string filterVal = "")
        {
            return _beklemeıptalnedeniDal.GetCount(filterCol, filterVal);
        }
    }
}