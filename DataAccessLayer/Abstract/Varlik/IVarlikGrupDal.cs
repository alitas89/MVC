using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.Concrete.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;

namespace DataAccessLayer.Abstract.Varlik
{
    public interface IVarlikGrupDal : IEntityRepository<VarlikGrup>
    {
        List<VarlikGrupDto> GetListDto();

        List<VarlikGrupDto> GetListPaginationDto(PagingParams pagingParams);
    }
}