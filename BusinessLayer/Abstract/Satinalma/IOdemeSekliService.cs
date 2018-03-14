using System.Collections.Generic;
using EntityLayer.Concrete.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;

namespace BusinessLayer.Abstract.Satinalma
{
    public interface IOdemeSekliService
    {
        List<OdemeSekli> GetList();

        OdemeSekli GetById(int id);

        int Add(OdemeSekli odemesekli);

        int Update(OdemeSekli odemesekli);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<OdemeSekli> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");
    }
}
