using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Abstract.Varlik
{
    public interface IVarlikTransferDal : IEntityRepository<VarlikTransfer>
    {
        List<VarlikTransferDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filterCol = "", string filterVal = "");
    }
}