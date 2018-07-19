using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Abstract.Varlik
{
    public interface IAracServisDal : IEntityRepository<AracServis>
    {
        List<AracServisDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        List<string> AddListWithTransactionBySablon(List<AracServis> listAracServis);
    }
}
