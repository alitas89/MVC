using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace BusinessLayer.Abstract.Malzeme
{
    public interface IAmbarService
    {
        List<Ambar> GetList();

        Ambar GetById(int id);

        int Add(Ambar ambar);

        int Update(Ambar ambar);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Ambar> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<AmbarDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");
    }
}