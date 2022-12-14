using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IParaBirimService
    {
        List<ParaBirim> GetList();

        ParaBirim GetById(int id);

        int Add(ParaBirim parabirim);

        int Update(ParaBirim parabirim);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<ParaBirim> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");
    }
}