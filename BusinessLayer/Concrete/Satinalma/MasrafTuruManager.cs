using System.Collections.Generic;
using BusinessLayer.Abstract.Satinalma;
using DataAccessLayer.Abstract.Satinalma;
using Core.Aspects.Postsharp.CacheAspects;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.DtoModel.SatinAlma;
using EntityLayer.Concrete.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;

namespace BusinessLayer.Concrete
{
    public class MasrafTuruManager : IMasrafTuruService
    {
        IMasrafTuruDal _masrafturuDal;

        public MasrafTuruManager(IMasrafTuruDal masrafturuDal)
        {
            _masrafturuDal = masrafturuDal;
        }

        
        [SecuredOperation(Roles = "Admin, SatinAlmaRead, MasrafTuruRead, MasrafTuruLtd")]
        public List<MasrafTuru> GetList()
        {
            return _masrafturuDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, SatinAlmaRead, MasrafTuruRead, MasrafTuruLtd")]
        public MasrafTuru GetById(int Id)
        {
            return _masrafturuDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, SatinAlmaCreate, MasrafTuruCreate")]
        public int Add(MasrafTuru masrafturu)
        {
            return _masrafturuDal.Add(masrafturu);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, SatinAlmaUpdate, MasrafTuruUpdate")]
        public int Update(MasrafTuru masrafturu)
        {
            return _masrafturuDal.Update(masrafturu);
        }

        
        [SecuredOperation(Roles = "Admin, SatinAlmaDelete, MasrafTuruDelete")]
        public int Delete(int Id)
        {
            return _masrafturuDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, SatinAlmaDelete, MasrafTuruDelete")]
        public int DeleteSoft(int Id)
        {
            return _masrafturuDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, SatinAlmaRead, MasrafTuruRead, MasrafTuruLtd")]
        public List<MasrafTuru> GetListPagination(PagingParams pagingParams)
        {
            return _masrafturuDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _masrafturuDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, SatinAlmaRead, MasrafTuruRead, MasrafTuruLtd")]
        public List<MasrafTuruDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _masrafturuDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _masrafturuDal.GetCountDto(filter);
        }
    }
}
