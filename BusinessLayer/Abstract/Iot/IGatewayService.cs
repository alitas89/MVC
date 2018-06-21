using EntityLayer.ComplexTypes.DtoModel.Iot;
using EntityLayer.ComplexTypes.ParameterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract.Iot
{
    public interface IGatewayService
    {        
        List<GatewayDto> GetListDto();

        List<GatewayDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        EntityLayer.Concrete.Iot.Gateway GetById(int id);
    }
}
