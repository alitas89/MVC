using EntityLayer.ComplexTypes.DtoModel.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using System.Collections.Generic;

namespace BusinessLayer.Abstract.Malzeme
{
    public interface IMalzemeService
    {
        List<EntityLayer.Concrete.Malzeme.Malzeme> GetList();

        EntityLayer.Concrete.Malzeme.Malzeme GetById(int id);

        int Add(EntityLayer.Concrete.Malzeme.Malzeme malzeme);

        int Update(EntityLayer.Concrete.Malzeme.Malzeme malzeme);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<MalzemeAmbarDetay> GetMalzemeAmbarDetay(int MalzemeID);

        List<EntityLayer.Concrete.Malzeme.Malzeme> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter);

        List<MalzemeDto> GetListDto();

        List<MalzemeDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");
    }
}
