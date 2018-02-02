using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IIsTipiService
    {
        List<IsTipi> GetList();

        IsTipi GetById(int id);

        int Add(IsTipi isTipi);

        int Update(IsTipi isTipi);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<IsTipi> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}