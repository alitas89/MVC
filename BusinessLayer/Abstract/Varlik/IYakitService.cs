using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IYakitService
    {
        List<Yakit> GetList();

        Yakit GetById(int id);

        int Add(Yakit yakit);

        int Update(Yakit yakit);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Yakit> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");
    }
}
