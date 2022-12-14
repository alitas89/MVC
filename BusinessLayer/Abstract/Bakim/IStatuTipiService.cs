using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IStatuTipiService
    {
        List<StatuTipi> GetList();

        StatuTipi GetById(int id);

        int Add(StatuTipi statutipi);

        int Update(StatuTipi statutipi);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<StatuTipi> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");
    }
}