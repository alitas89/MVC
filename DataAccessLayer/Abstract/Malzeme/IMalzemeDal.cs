using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Malzeme
{
    public interface IMalzemeDal : IEntityRepository<EntityLayer.Concrete.Malzeme.Malzeme>
    {
        List<MalzemeDto> GetListDto();

        List<MalzemeDto> GetListPaginationDto(PagingParams pagingParams);

        List<MalzemeAmbarDetay> GetMalzemeAmbarDetay(int MalzemeID);

        int GetCountDto(string filter = "");

        bool IsKodDefined(string Kod);

        List<string> AddListWithTransactionBySablon(List<Malzeme> listMalzeme);
    }
}
