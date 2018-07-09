using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;
using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Iot;

namespace DataAccessLayer.Abstract.Iot
{
    public interface ISayacDal : IEntityRepository<Sayac>
    {
        List<SayacDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        List<SayacDto> GetListPaginationDtoByModemSeriNo(PagingParams pagingParams, string modemserino);

        int GetCountDtoByModemSeriNo(string modemserino = "", string filter = "");
    }
}
