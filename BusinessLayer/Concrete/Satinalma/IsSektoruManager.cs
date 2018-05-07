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
    public class IsSektoruManager : IIsSektoruService
    {
        IIsSektoruDal _issektoruDal;

        public IsSektoruManager(IIsSektoruDal ıssektoruDal)
        {
            _issektoruDal = ıssektoruDal;
        }

        
        [SecuredOperation(Roles = "Admin, SatinAlmaRead, IsSektoruRead, IsSektoruLtd")]
        public List<IsSektoru> GetList()
        {
            return _issektoruDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, SatinAlmaRead, IsSektoruRead, IsSektoruLtd")]
        public IsSektoru GetById(int Id)
        {
            return _issektoruDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, SatinAlmaCreate, IsSektoruCreate")]
        public int Add(IsSektoru ıssektoru)
        {
            return _issektoruDal.Add(ıssektoru);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, SatinAlmaUpdate, IsSektoruUpdate")]
        public int Update(IsSektoru ıssektoru)
        {
            return _issektoruDal.Update(ıssektoru);
        }

        
        [SecuredOperation(Roles = "Admin, SatinAlmaDelete, IsSektoruDelete")]
        public int Delete(int Id)
        {
            return _issektoruDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, SatinAlmaDelete, IsSektoruDelete")]
        public int DeleteSoft(int Id)
        {
            return _issektoruDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, SatinAlmaRead, IsSektoruRead, IsSektoruLtd")]
        public List<IsSektoru> GetListPagination(PagingParams pagingParams)
        {
            return _issektoruDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _issektoruDal.GetCount(filter);
        }

    }
}
