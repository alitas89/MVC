using EntityLayer.ComplexTypes.DtoModel.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;
using System.Collections.Generic;

namespace BusinessLayer.Abstract.Malzeme
{
    public interface IMalzemeHareketService
    {
        List<MalzemeHareket> GetList();

        MalzemeHareket GetById(int id);

        int Add(MalzemeHareket malzemehareket);

        int Update(MalzemeHareket malzemehareket);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<MalzemeHareket> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<MalzemeHareketDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        int AddMalzemeHareket(MalzemeHareketTemp malzemeHareketTemp);

        int UpdateMalzemeHareket(MalzemeHareketTemp malzemeHareketTemp);
    }
}
