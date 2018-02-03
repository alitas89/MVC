using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace BusinessLayer.Abstract.Personel
{
    public interface IMesaiService
    {
        List<Mesai> GetList();

        Mesai GetById(int id);

        int Add(Mesai mesai);

        int Update(Mesai mesai);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Mesai> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}