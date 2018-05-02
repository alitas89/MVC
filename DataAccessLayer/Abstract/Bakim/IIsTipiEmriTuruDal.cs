using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IIsTipiEmirTuruDal : IEntityRepository<IsTipiEmirTuru>
    {
        List<IsTipiEmirTuruDto> GetList(int IsTipiID);

        int AddWithTransaction(int IsTipiID, List<int> listIsTipiEmirTuru);

        int UpdateWithTransaction(int IsTipiID, List<int> listIsTipiEmirTuru);

        List<IsTipiEmirTuruDto> GetListPaginationDto(int IsTipiID, PagingParams pagingParams);

        int GetCountDto(int IsTipiID, string filter = "");

    }
}
