using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace BusinessLayer.Abstract.Sistem
{
    public interface IBildirimIsTalebiSonucService
    {
        List<BildirimIsTalebiSonuc> GetList();

        BildirimIsTalebiSonuc GetById(int id);

        int Add(BildirimIsTalebiSonuc bildirimıstalebisonuc);

        int Update(BildirimIsTalebiSonuc bildirimıstalebisonuc);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<BildirimIsTalebiSonuc> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter="");
    }

}