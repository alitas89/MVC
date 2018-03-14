using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IBirimService
    {
        List<Birim> GetList();

        Birim GetById(int id);

        int Add(Birim birim);

        int Update(Birim birim);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Birim> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");
    }
}