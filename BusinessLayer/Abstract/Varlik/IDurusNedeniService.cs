using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IDurusNedeniService
    {
        List<DurusNedeni> GetList();

        DurusNedeni GetById(int id);

        int Add(DurusNedeni durusnedeni);

        int Update(DurusNedeni durusnedeni);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<DurusNedeni> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}