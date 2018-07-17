using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IVarlikSablonService
    {
        List<VarlikSablon> GetList();

        VarlikSablon GetById(int id);

        int Add(VarlikSablon varliksablon);

        int Update(VarlikSablon varliksablon);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<VarlikSablon> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter);

        List<VarlikSablonDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        List<string> AddListWithTransactionBySablon(List<VarlikSablon> listVarlikSablon);
    }
}
