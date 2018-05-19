using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IBakimDurumuService
    {
        List<BakimDurumu> GetList();

        BakimDurumu GetById(int id);

        int Add(BakimDurumu bakimdurumu);

        int Update(BakimDurumu bakimdurumu);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<BakimDurumu> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter="");
    }

}