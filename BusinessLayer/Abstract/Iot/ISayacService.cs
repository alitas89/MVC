using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.ComplexTypes.DtoModel.Iot;

namespace BusinessLayer.Abstract.Iot
{
    public interface ISayacService
    {
        List<SayacDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        List<SayacDto> GetListPaginationDtoByModemSeriNo(PagingParams pagingParams, string modemSeriNo);

        int GetCountDtoByModemSeriNo(string modemserino, string filter = "");
    }
}
