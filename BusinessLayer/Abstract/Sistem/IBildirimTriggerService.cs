using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace BusinessLayer.Abstract.Sistem
{
    public interface IBildirimTriggerService
    {
        List<BildirimTrigger> GetList();

        BildirimTrigger GetById(int id);

        int Add(BildirimTrigger bildirimtrigger);

        int Update(BildirimTrigger bildirimtrigger);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<BildirimTrigger> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter="");
    }

}