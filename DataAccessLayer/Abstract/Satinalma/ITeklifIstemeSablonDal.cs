using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.SatinAlma;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Satinalma;

namespace DataAccessLayer.Abstract.Satinalma
{
    public interface ITeklifIstemeSablonDal : IEntityRepository<TeklifIstemeSablon>
    {
        List<TeklifIstemeSablonDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");
    }
}
