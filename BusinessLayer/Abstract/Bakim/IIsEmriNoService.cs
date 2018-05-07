using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IIsEmriNoService
    {
        List<IsEmriNo> GetList();

        IsEmriNo GetById(int id);

        int Add(IsEmriNo ısemrino);

        int Update(IsEmriNo ısemrino);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<IsEmriNo> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter="");
    }
}