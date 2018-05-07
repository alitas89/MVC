using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;
using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Personel;
using EntityLayer.ComplexTypes.DtoModel.Varlik;

namespace BusinessLayer.Abstract.Personel
{
    public interface IKaynakService
    {
        List<Kaynak> GetList();

        Kaynak GetById(int id);

        int Add(Kaynak kaynak);

        int Update(Kaynak kaynak);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Kaynak> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<KaynakDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");
    }
}
