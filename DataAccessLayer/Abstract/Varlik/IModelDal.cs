using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.Concrete.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;

namespace DataAccessLayer.Abstract.Varlik
{
    public interface IModelDal : IEntityRepository<Model>
    {
        List<ModelDto> GetListDto();

        List<ModelDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        List<string> AddListWithTransactionBySablon(List<Model> listModel);

    }
}