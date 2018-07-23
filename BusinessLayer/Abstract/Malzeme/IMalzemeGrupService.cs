using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace BusinessLayer.Abstract.Malzeme
{
    public interface IMalzemeGrupService
    {
        List<MalzemeGrup> GetList();

        MalzemeGrup GetById(int id);

        int Add(MalzemeGrup malzemegrup);

        int Update(MalzemeGrup malzemegrup);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<MalzemeGrup> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<string> AddListWithTransactionBySablon(List<MalzemeGrup> listMalzemeGrup);
    }
}