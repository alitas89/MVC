using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IMarkaService
    {
        List<Marka> GetList();

        Marka GetById(int id);

        int Add(Marka marka);

        int Update(Marka marka);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Marka> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<string> AddListWithTransactionBySablon(List<Marka> listMarka);

    }
}