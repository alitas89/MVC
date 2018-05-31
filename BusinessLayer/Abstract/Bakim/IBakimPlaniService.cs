using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IBakimPlaniService
    {
        List<BakimPlani> GetList();

        BakimPlani GetById(int id);

        int Add(BakimPlani bakimplani);

        int Update(BakimPlani bakimplani);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<BakimPlani> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter="");

        int AddWithTransaction(BakimPlani bakimplani, List<IsAdimlari> listIsAdimlari);

        int UpdateWithTransaction(BakimPlani bakimplani, List<IsAdimlari> listIsAdimlari);
        
    }
}