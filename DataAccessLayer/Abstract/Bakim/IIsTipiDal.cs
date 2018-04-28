using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IIsTipiDal : IEntityRepository<IsTipi>
    {
        List<IsTipiDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");        
    }
}