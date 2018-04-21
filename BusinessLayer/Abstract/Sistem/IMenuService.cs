using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace BusinessLayer.Abstract.Sistem
{
    public interface IMenuService
    {
        List<Menu> GetList();

        Menu GetById(int id);

        int Add(Menu menu);

        int Update(Menu menu);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Menu> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter="");

        string GetMenuByKod(string arrKodJson);

        string GetMenuByRoles(List<string> listRoles);
    }
}