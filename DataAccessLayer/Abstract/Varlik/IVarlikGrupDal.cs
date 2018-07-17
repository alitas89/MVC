using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.Concrete.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;

namespace DataAccessLayer.Abstract.Varlik
{
    public interface IVarlikGrupDal : IEntityRepository<VarlikGrup>
    {
        List<VarlikGrupDto> GetListDto();

        List<VarlikGrupDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        List<string> AddListWithTransactionBySablon(List<VarlikGrup> listVarlikGrup);
    }
}