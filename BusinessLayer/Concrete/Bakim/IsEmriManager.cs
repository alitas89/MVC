using System.Collections.Generic;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class IsEmriManager : IIsEmriService
    {
        IIsEmriDal _isEmriDal;

        public IsEmriManager(IIsEmriDal ısemriDal)
        {
            _isEmriDal = ısemriDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, IsEmriRead, IsEmriLtd")]
        public List<IsEmri> GetList()
        {
            return _isEmriDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, BakimRead, IsEmriRead, IsEmriLtd")]
        public IsEmri GetById(int Id)
        {
            return _isEmriDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, IsEmriCreate")]
        public int Add(IsEmri ısemri)
        {
            return _isEmriDal.Add(ısemri);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, IsEmriUpdate")]
        public int Update(IsEmri ısemri)
        {
            return _isEmriDal.Update(ısemri);
        }

        
        [SecuredOperation(Roles = "Admin,Editor, BakimDelete, IsEmriDelete")]
        public int Delete(int Id)
        {
            return _isEmriDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, IsEmriDelete")]
        public int DeleteSoft(int Id)
        {
            return _isEmriDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, IsEmriRead, IsEmriLtd")]
        public List<IsEmri> GetListPagination(PagingParams pagingParams)
        {
            return _isEmriDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _isEmriDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, IsEmriRead, IsEmriLtd")]
        public List<IsEmriDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _isEmriDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _isEmriDal.GetCountDto(filter);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, IsEmriRead, IsEmriLtd")]
        public List<IsTipiForKullaniciTemp> GetIsTipiListByKullaniciID(int KullaniciID)
        {
            return _isEmriDal.GetIsTipiListByKullaniciID(KullaniciID);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, IsEmriRead, IsEmriLtd")]
        public List<IsEmriDto> GetListPaginationDtoByKullaniciID(PagingParams pagingParams, int KullaniciID)
        {
            return _isEmriDal.GetListPaginationDtoByKullaniciID(pagingParams, KullaniciID);
        }

        public int GetCountDtoByKullaniciID(int KullaniciID, string filter = "")
        {
            return _isEmriDal.GetCountDtoByKullaniciID(KullaniciID, filter);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, IsEmriRead, IsEmriLtd")]
        public List<IsEmriNo> GetIsEmriNoByIsEmriID(int IsEmriID)
        {
            return _isEmriDal.GetIsEmriNoByIsEmriID(IsEmriID);
        }
    }
}