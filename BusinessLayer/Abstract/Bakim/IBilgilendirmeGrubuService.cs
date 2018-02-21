using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IBilgilendirmeGrubuService
    {
        List<BilgilendirmeGrubu> GetList();

        BilgilendirmeGrubu GetById(int id);

        int Add(BilgilendirmeGrubu bilgilendirmegrubu);

        int Update(BilgilendirmeGrubu bilgilendirmegrubu);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<BilgilendirmeGrubu> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");

        List<BilgilendirmeGrubuDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filterCol = "", string filterVal = "");
    }
}