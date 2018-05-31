using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IIsinSorumlusuDal : IEntityRepository<IsEmri>
    {
        IsEmriDto GetByKullaniciID(int IsEmriID, int KullaniciID);

        List<IsEmriDto> GetListPaginationDtoByKullaniciID(PagingParams pagingParams, int KullaniciID);

        int GetCountDtoByKullaniciID(int KullaniciID, string filter = "");

        int GetEditYetki(int IsEmriID, int KullaniciID);

    }
}