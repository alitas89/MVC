using System.Collections.Generic;
using EntityLayer.Concrete.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;

namespace BusinessLayer.Abstract.Satinalma
{
    public interface IIsSektoruService
    {
        List<IsSektoru> GetList();

        IsSektoru GetById(int id);

        int Add(IsSektoru ıssektoru);

        int Update(IsSektoru ıssektoru);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<IsSektoru> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");
    }
}