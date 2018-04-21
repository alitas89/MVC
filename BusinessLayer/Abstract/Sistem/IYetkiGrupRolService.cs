using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace BusinessLayer.Abstract.Sistem
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

        string GetYetkiRolByYetkiGrupID(int YetkiGrupID);

        int DeleteSoftByYetkiGrupID(int YetkiGrupID);

        int AddYetkiGrupRoles(int yetkiGrupID, string arrYetkiRol);
    }
}