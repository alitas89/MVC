using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace BusinessLayer.Abstract.Malzeme
{
    public interface IOlcuBirimService
    {
        List<OlcuBirim> GetList();

        OlcuBirim GetById(int id);

        int Add(OlcuBirim olcubirim);

        int Update(OlcuBirim olcubirim);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<OlcuBirim> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<string> AddListWithTransactionBySablon(List<OlcuBirim> listOlcuBirim);
    }
}