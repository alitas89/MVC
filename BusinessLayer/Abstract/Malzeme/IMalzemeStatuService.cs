using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace BusinessLayer.Abstract.Malzeme
{
    public interface IMalzemeStatuService
    {
        List<MalzemeStatu> GetList();

        MalzemeStatu GetById(int id);

        int Add(MalzemeStatu malzemestatu);

        int Update(MalzemeStatu malzemestatu);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<MalzemeStatu> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<string> AddListWithTransactionBySablon(List<MalzemeStatu> listMalzemeStatu);
    }
}