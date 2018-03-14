using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.Concrete.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;

namespace DataAccessLayer.Abstract.Varlik
{
    public interface IKisimDal : IEntityRepository<Kisim>
    {
        List<KisimDto> GetListDto();

        List<KisimDto> GetListPaginationDto(PagingParams pagingParams);

        List<Kisim> GetList(int SarfYeriID);

        bool IsKodDefined(string Kod);

        int GetCountDto(string filter = "");
    }
}