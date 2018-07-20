using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IBakimRiskiDal : IEntityRepository<BakimRiski>
    {
        List<BakimRiskiDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        List<BakimRiski> GetListBakimRiskiByPeriyodikBakimID(int PeriyodikBakimID);

        List<string> AddListWithTransactionBySablon(List<BakimRiski> listBakimRiski);
    }
}