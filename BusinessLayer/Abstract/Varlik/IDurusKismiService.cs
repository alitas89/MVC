using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IDurusKismiService
    {
        List<DurusKismi> GetList();

        DurusKismi GetById(int id);

        int Add(DurusKismi duruskismi);

        int Update(DurusKismi duruskismi);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<DurusKismi> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}