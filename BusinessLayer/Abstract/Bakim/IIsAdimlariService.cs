using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IIsAdimlariService
    {
        List<IsAdimlari> GetList();

        IsAdimlari GetById(int id);

        int Add(IsAdimlari ısadimlari);

        int Update(IsAdimlari ısadimlari);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<IsAdimlari> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter="");
    }
}