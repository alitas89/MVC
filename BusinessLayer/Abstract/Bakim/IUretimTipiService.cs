using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IUretimTipiService
    {
        List<UretimTipi> GetList();

        UretimTipi GetById(int id);

        int Add(UretimTipi uretimtipi);

        int Update(UretimTipi uretimtipi);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<UretimTipi> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}