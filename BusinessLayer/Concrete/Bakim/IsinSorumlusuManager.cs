using System.Collections.Generic;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class IsinSorumlusuManager : IIsinSorumlusuService
    {
        IIsinSorumlusuDal _isinSorumlusuDal;

        public IsinSorumlusuManager(IIsinSorumlusuDal isinSorumlusuDal)
        {
            _isinSorumlusuDal = isinSorumlusuDal;
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Authorized")]
        public int Update(IsEmri ısemri)
        {
            if (ısemri.StatuID == 15)
            {
                return _isinSorumlusuDal.Update(ısemri);
            }
            return 0;
        }

        [SecuredOperation(Roles = "Authorized")]
        public IsEmriDto GetByKullaniciID(int IsEmriID, int KullaniciID)
        {
            return _isinSorumlusuDal.GetByKullaniciID(IsEmriID, KullaniciID);
        }

        [SecuredOperation(Roles = "Authorized")]
        public List<IsEmriDto> GetListPaginationDtoByKullaniciID(PagingParams pagingParams, int KullaniciID)
        {
            return _isinSorumlusuDal.GetListPaginationDtoByKullaniciID(pagingParams, KullaniciID);
        }

        public int GetCountDtoByKullaniciID(int KullaniciID, string filter = "")
        {
            return _isinSorumlusuDal.GetCountDtoByKullaniciID(KullaniciID, filter);
        }

        [SecuredOperation(Roles = "Authorized")]
        public int GetEditYetki(int IsEmriID, int KullaniciID)
        {
            return _isinSorumlusuDal.GetEditYetki(IsEmriID, KullaniciID);
        }
    }
}