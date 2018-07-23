using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Personel
{
    public interface IMesaiDal : IEntityRepository<Mesai>
    {
        List<MesaiDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        List<string> AddListWithTransactionBySablon(List<Mesai> listMesai);
    }
}