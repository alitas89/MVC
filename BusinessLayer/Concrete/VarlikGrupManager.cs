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
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class VarlikGrupManager : IVarlikGrupService
    {
        IVarlikGrupDal _varlikgrupDal;

        public VarlikGrupManager(IVarlikGrupDal varlikgrupDal)
        {
            _varlikgrupDal = varlikgrupDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<VarlikGrup> GetList()
        {
            return _varlikgrupDal.GetList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<VarlikGrupDto> GetListDto()
        {
            return _varlikgrupDal.GetListDto();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public VarlikGrup GetById(int Id)
        {
            return _varlikgrupDal.Get(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [FluentValidationAspect(typeof(VarlikGrupValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(VarlikGrup varlikgrup)
        {
            return _varlikgrupDal.Add(varlikgrup);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [FluentValidationAspect(typeof(VarlikGrupValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(VarlikGrup varlikgrup)
        {
            return _varlikgrupDal.Update(varlikgrup);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _varlikgrupDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _varlikgrupDal.DeleteSoft(Id);
        }
    }
}
