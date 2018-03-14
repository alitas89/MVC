using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract.Personel;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace BusinessLayer.Concrete.Personel
{
    public class VardiyaSinifiManager : IVardiyaSinifiService
    {
        IVardiyaSinifiDal _vardiyasinifiDal;

        public VardiyaSinifiManager(IVardiyaSinifiDal vardiyasinifiDal)
        {
            _vardiyasinifiDal = vardiyasinifiDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<VardiyaSinifi> GetList()
        {
            return _vardiyasinifiDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public VardiyaSinifi GetById(int Id)
        {
            return _vardiyasinifiDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(VardiyaSinifi vardiyasinifi)
        {
            return _vardiyasinifiDal.Add(vardiyasinifi);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(VardiyaSinifi vardiyasinifi)
        {
            return _vardiyasinifiDal.Update(vardiyasinifi);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _vardiyasinifiDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _vardiyasinifiDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<VardiyaSinifi> GetListPagination(PagingParams pagingParams)
        {
            return _vardiyasinifiDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _vardiyasinifiDal.GetCount(filter);
        }

    }
}
