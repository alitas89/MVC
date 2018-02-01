using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IGecikmeNedeniService
    {
        List<GecikmeNedeni> GetList();

        GecikmeNedeni GetById(int id);

        int Add(GecikmeNedeni gecikmenedeni);

        int Update(GecikmeNedeni gecikmenedeni);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<GecikmeNedeni> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}