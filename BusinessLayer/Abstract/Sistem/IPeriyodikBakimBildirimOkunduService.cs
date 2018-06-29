using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace BusinessLayer.Abstract.Sistem
{
    public interface IPeriyodikBakimBildirimOkunduService
    {
        List<PeriyodikBakimBildirimOkundu> GetList();

        PeriyodikBakimBildirimOkundu GetById(int id);

        int Add(PeriyodikBakimBildirimOkundu periyodikbakimbildirimokundu);

        int Update(PeriyodikBakimBildirimOkundu periyodikbakimbildirimokundu);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<PeriyodikBakimBildirimOkundu> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter="");
    }
}