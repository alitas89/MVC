using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IAracServisService
    {
        List<AracServis> GetList();

        AracServis GetById(int id);

        int Add(AracServis aracservis);

        int Update(AracServis aracservis);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<AracServis> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}
