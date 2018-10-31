using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.ComplexTypes.DtoModel.Iot;
using EntityLayer.ComplexTypes.DtoModel.Varlik;

namespace BusinessLayer.Abstract.Iot
{
    public interface ISayacService
    {
        List<SayacDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        List<SayacDto> GetListPaginationDtoByBagliVarlikKod(PagingParams pagingParams, int baglivarlikkod);

        int GetCountDtoByBagliVarlikKod(int baglivarlikkod, string filter = "");

        int AddSayacKomut(SayacKomut sarfyeri);
    }
}
