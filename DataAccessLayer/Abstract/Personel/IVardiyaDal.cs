using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;
using EntityLayer.Concrete.Personel;

namespace DataAccessLayer.Abstract.Personel
{
    public interface IVardiyaDal : IEntityRepository<Vardiya>
    {
        List<VardiyaDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filterCol = "", string filterVal = "");
    }
}