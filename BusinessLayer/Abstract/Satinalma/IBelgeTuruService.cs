using System.Collections.Generic;
using EntityLayer.Concrete.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;

namespace BusinessLayer.Abstract.Satinalma
{
    public interface IBelgeTuruService
    {
        List<BelgeTuru> GetList();

        BelgeTuru GetById(int id);

        int Add(BelgeTuru belgeturu);

        int Update(BelgeTuru belgeturu);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<BelgeTuru> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");
    }
}