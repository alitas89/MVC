using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;
using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Iot;
using EntityLayer.ComplexTypes.DtoModel.Varlik;

namespace DataAccessLayer.Abstract.Iot
{
    public interface ISayacDal : IEntityRepository<Sayac>
    {
        List<SayacDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        List<SayacDto> GetListPaginationDtoByBagliVarlikKod(PagingParams pagingParams, int baglivarlikkod);

        int GetCountDtoByGetBagliVarlikKod(int baglivarlikkod , string filter = "");

        int AddSayacKomut(SayacKomut sayacKomut);
    }
}
