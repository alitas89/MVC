using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IIsEmriService
    {
        List<IsEmri> GetList();

        IsEmri GetById(int id);

        int Add(IsEmri isemri);

        int Update(IsEmri isemri);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<IsEmri> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "");

        List<IsEmriDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");
    }
}