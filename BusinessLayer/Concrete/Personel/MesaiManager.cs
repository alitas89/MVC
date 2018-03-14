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
    public class MesaiManager : IMesaiService
    {
        IMesaiDal _mesaiDal;

        public MesaiManager(IMesaiDal mesaiDal)
        {
            _mesaiDal = mesaiDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<Mesai> GetList()
        {
            return _mesaiDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public Mesai GetById(int Id)
        {
            return _mesaiDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(Mesai mesai)
        {
            return _mesaiDal.Add(mesai);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(Mesai mesai)
        {
            return _mesaiDal.Update(mesai);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _mesaiDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _mesaiDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<Mesai> GetListPagination(PagingParams pagingParams)
        {
            return _mesaiDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _mesaiDal.GetCount(filter);
        }

    }
}
