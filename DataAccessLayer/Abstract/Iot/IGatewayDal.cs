using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Iot;
using EntityLayer.ComplexTypes.ParameterModel;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Iot
{
    public interface IGatewayDal: IEntityRepository<EntityLayer.Concrete.Iot.Gateway>
    {
        List<GatewayDto> GetListDto();

        List<GatewayDto> GetListPaginationDto(PagingParams pagingParams);
        
        int GetCountDto(string filter = "");
    }
}
