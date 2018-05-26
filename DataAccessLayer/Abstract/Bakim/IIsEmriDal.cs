using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IIsEmriDal : IEntityRepository<IsEmri>
    {
        List<IsEmriDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        List<IsTipiForKullaniciTemp> GetIsTipiListByKullaniciID(int KullaniciID);

        List<IsEmriDto> GetListPaginationDtoByKullaniciID(PagingParams pagingParams, int KullaniciID);

        int GetCountDtoByKullaniciID(int KullaniciID, string filter = "");

        List<IsEmriNo> GetIsEmriNoByIsEmriID(int IsEmriID);

        int GetEditYetki(int IsEmriID, int KullaniciID);

        int AddWithTransaction(IsEmri isemri);
    }
}