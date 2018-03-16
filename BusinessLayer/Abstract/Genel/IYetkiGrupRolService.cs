using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Genel;

namespace BusinessLayer.Abstract.Genel
{
    public interface IYetkiGrupRolService
    {
        List<YetkiGrupRol> GetList();

        YetkiGrupRol GetById(int id);

        int Add(YetkiGrupRol yetkigruprol);

        int Update(YetkiGrupRol yetkigruprol);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<YetkiGrupRol> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<YetkiGrupRol> GetListByGrupId(int YetkiGrupID);
    }
}