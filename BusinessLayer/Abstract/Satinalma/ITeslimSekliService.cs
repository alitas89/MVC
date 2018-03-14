using System.Collections.Generic;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;

namespace BusinessLayer.Abstract.Satinalma
{
    public interface ITeslimSekliService
    {
        List<TeslimSekli> GetList();

        TeslimSekli GetById(int id);

        int Add(TeslimSekli teslimsekli);

        int Update(TeslimSekli teslimsekli);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<TeslimSekli> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");
    }
}