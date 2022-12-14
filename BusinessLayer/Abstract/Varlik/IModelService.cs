using System.Collections.Generic;
using System.Data;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IModelService
    {
        List<Model> GetList();

        Model GetById(int id);

        int Add(Model model);

        int Update(Model model);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<ModelDto> GetListDto();

        List<Model> GetListPagination(PagingParams pagingParams);

        List<ModelDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCount(string filter = "");

        int GetCountDto(string filter = "");

        List<string> AddListWithTransactionBySablon(List<Model> listModel);

        List<Model> ExcelDataProcess(DataTable dataTable);

    }
}