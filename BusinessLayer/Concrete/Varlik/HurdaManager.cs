using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Varlik;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Concrete.Varlik
{
    public class HurdaManager : IHurdaService
    {
        IHurdaDal _hurdaDal;

        public HurdaManager(IHurdaDal hurdaDal)
        {
            _hurdaDal = hurdaDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikRead, HurdaRead, HurdaLtd")]
        public List<Hurda> GetList()
        {
            return _hurdaDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, HurdaRead, HurdaLtd")]
        public Hurda GetById(int Id)
        {
            return _hurdaDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikCreate, HurdaCreate")]
        public int Add(Hurda hurda)
        {
            return _hurdaDal.Add(hurda);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikUpdate, HurdaUpdate")]
        public int Update(Hurda hurda)
        {
            return _hurdaDal.Update(hurda);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikDelete, HurdaDelete")]
        public int Delete(int Id)
        {
            return _hurdaDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikDelete, HurdaDelete")]
        public int DeleteSoft(int Id)
        {
            return _hurdaDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, HurdaRead, HurdaLtd")]
        public List<Hurda> GetListPagination(PagingParams pagingParams)
        {
            return _hurdaDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _hurdaDal.GetCount(filter);
        }
    }
}
