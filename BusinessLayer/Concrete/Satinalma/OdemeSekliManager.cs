using System.Collections.Generic;
using BusinessLayer.Abstract.Satinalma;
using DataAccessLayer.Abstract.Satinalma;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Aspects.Postsharp.CacheAspects;
using Core.Aspects.Postsharp.AuthorizationAspects;
using EntityLayer.Concrete.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;

namespace BusinessLayer.Concrete.Satinalma
{
    public class OdemeSekliManager : IOdemeSekliService
    {
        IOdemeSekliDal _odemesekliDal;

        public OdemeSekliManager(IOdemeSekliDal odemesekliDal)
        {
            _odemesekliDal = odemesekliDal;
        }

        
        [SecuredOperation(Roles = "Admin, SatinAlmaRead, OdemeSekliRead, OdemeSekliLtd")]
        public List<OdemeSekli> GetList()
        {
            return _odemesekliDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, SatinAlmaRead, OdemeSekliRead, OdemeSekliLtd")]
        public OdemeSekli GetById(int Id)
        {
            return _odemesekliDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, SatinAlmaCreate, OdemeSekliCreate")]
        public int Add(OdemeSekli odemesekli)
        {
            return _odemesekliDal.Add(odemesekli);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, SatinAlmaUpdate, OdemeSekliUpdate")]
        public int Update(OdemeSekli odemesekli)
        {
            return _odemesekliDal.Update(odemesekli);
        }

        
        [SecuredOperation(Roles = "Admin, SatinAlmaDelete, OdemeSekliDelete")]
        public int Delete(int Id)
        {
            return _odemesekliDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, SatinAlmaDelete, OdemeSekliDelete")]
        public int DeleteSoft(int Id)
        {
            return _odemesekliDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, SatinAlmaRead, OdemeSekliRead, OdemeSekliLtd")]
        public List<OdemeSekli> GetListPagination(PagingParams pagingParams)
        {
            return _odemesekliDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _odemesekliDal.GetCount(filter);
        }

    }
}
