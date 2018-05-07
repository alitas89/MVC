using System.Collections.Generic;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class IsEmriNoManager : IIsEmriNoService
    {
        IIsEmriNoDal _ısemrinoDal;

        public IsEmriNoManager(IIsEmriNoDal ısemrinoDal)
        {
            _ısemrinoDal = ısemrinoDal;
        }

        
        [SecuredOperation(Roles = "Admin, IsEmriNoRead, IsEmriNoLtd")]
        public List<IsEmriNo> GetList()
        {
            return _ısemrinoDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, IsEmriNoRead, IsEmriNoLtd")]
        public IsEmriNo GetById(int Id)
        {
            return _ısemrinoDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, IsEmriNoCreate")]
        public int Add(IsEmriNo ısemrino)
        {
            return _ısemrinoDal.Add(ısemrino);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, IsEmriNoUpdate")]
        public int Update(IsEmriNo ısemrino)
        {
            return _ısemrinoDal.Update(ısemrino);
        }

        
        [SecuredOperation(Roles = "Admin, IsEmriNoDelete")]
        public int Delete(int Id)
        {
            return _ısemrinoDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, IsEmriNoDelete")]
        public int DeleteSoft(int Id)
        {
            return _ısemrinoDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, IsEmriNoRead, IsEmriNoLtd")]
        public List<IsEmriNo> GetListPagination(PagingParams pagingParams)
        {
            return _ısemrinoDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _ısemrinoDal.GetCount(filter);
        }

    }
}