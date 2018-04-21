using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace BusinessLayer.Abstract.Sistem
{
    public interface IYetkiGrupService
    {
        List<YetkiGrup> GetList();

        YetkiGrup GetById(int id);

        int Add(YetkiGrup yetkigrup);

        int Update(YetkiGrup yetkigrup);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<YetkiGrup> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");
    }
}