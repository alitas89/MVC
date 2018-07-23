using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Malzeme
{
    public interface IMalzemeSeriNoDal : IEntityRepository<MalzemeSeriNo>
    {
        List<MalzemeSeriNoDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        List<string> AddListWithTransactionBySablon(List<MalzemeSeriNo> listMalzemeSeriNo);
    }
}