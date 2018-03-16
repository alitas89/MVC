using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Genel;

namespace BusinessLayer.Abstract.Genel
{
    public interface IYetkiRolService
    {
        List<YetkiRol> GetList();

        YetkiRol GetById(int id);

        int Add(YetkiRol yetkirol);

        int Update(YetkiRol yetkirol);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<YetkiRol> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");
    }
}