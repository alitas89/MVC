using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace BusinessLayer.Abstract.Sistem
{
    public interface IPeriyodikBakimBildirimPushedService
    {
        List<PeriyodikBakimBildirimPushed> GetList();

        PeriyodikBakimBildirimPushed GetById(int id);

        int Add(PeriyodikBakimBildirimPushed periyodikbakimbildirimpushed);

        int Update(PeriyodikBakimBildirimPushed periyodikbakimbildirimpushed);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<PeriyodikBakimBildirimPushed> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter="");
    }
}