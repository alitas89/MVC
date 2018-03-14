using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Varlik;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IAkaryakitAlimFisService
    {
        List<AkaryakitAlimFis> GetList();

        AkaryakitAlimFis GetById(int id);

        int Add(AkaryakitAlimFis akaryakitalimfis);

        int Update(AkaryakitAlimFis akaryakitalimfis);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<AkaryakitAlimFis> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<AkaryakitAlimFisDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");
    }
}
