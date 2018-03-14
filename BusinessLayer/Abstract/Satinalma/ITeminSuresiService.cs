using System.Collections.Generic;
using EntityLayer.Concrete.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;

namespace BusinessLayer.Abstract.Satinalma
{
    public interface ITeminSuresiService
    {
        List<TeminSuresi> GetList();

        TeminSuresi GetById(int id);

        int Add(TeminSuresi teminsuresi);

        int Update(TeminSuresi teminsuresi);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<TeminSuresi> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");
    }
}