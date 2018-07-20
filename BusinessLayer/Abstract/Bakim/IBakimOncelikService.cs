using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IBakimOncelikService
    {
        List<BakimOncelik> GetList();

        BakimOncelik GetById(int id);

        int Add(BakimOncelik bakimoncelik);

        int Update(BakimOncelik bakimoncelik);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<BakimOncelik> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<string> AddListWithTransactionBySablon(List<BakimOncelik> listBakimOncelik);
    }
}