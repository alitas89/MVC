using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IIsTalebiDal : IEntityRepository<IsTalebi>
    {
        List<IsTalebiDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        List<IsTipiForKullaniciTemp> GetIsTipiListByKullaniciID(int KullaniciID);

        List<EmirTuruIsTipiTemp> GetEmirTuruListByIsTipiID(int IsTipiID);

        List<IsEmriNo> GetIsEmriNoByIsTalepID(int IsTalepID);

        int AddWithTransaction(IsTalebi ıstalebi);

        List<IsTalebiDto> GetListPaginationDtoKullaniciID(PagingParams pagingParams, int KullaniciID);
    }
}
