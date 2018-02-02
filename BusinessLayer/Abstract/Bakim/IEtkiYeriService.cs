using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IEtkiYeriService
    {
        List<EtkiYeri> GetList();

        EtkiYeri GetById(int id);

        int Add(EtkiYeri etkiyeri);

        int Update(EtkiYeri etkiyeri);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<EtkiYeri> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}