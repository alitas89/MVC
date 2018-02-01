using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IArizaNedeniService
    {
        List<ArizaNedeni> GetList();

        ArizaNedeni GetById(int id);

        int Add(ArizaNedeni arizanedeni);

        int Update(ArizaNedeni arizanedeni);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<ArizaNedeni> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}