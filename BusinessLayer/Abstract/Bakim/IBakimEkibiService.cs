using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IBakimEkibiService
    {
        List<BakimEkibi> GetList();

        BakimEkibi GetById(int id);

        int Add(BakimEkibi bakimekibi);

        int Update(BakimEkibi bakimekibi);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<BakimEkibi> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");
    }
}