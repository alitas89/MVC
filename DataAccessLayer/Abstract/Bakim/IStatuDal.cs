using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IStatuDal : IEntityRepository<Statu>
    {
        List<StatuDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        List<string> AddListWithTransactionBySablon(List<Statu> listStatu);
    }
}