using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IPeriyodikBakimService
    {
        List<PeriyodikBakim> GetList();

        PeriyodikBakim GetById(int id);

        int Add(PeriyodikBakim periyodikbakim);

        int Update(PeriyodikBakim periyodikbakim);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<PeriyodikBakim> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter="");

        int UpdateWithTransaction(PeriyodikBakim periyodikBakim, List<int> listBakimPlani, 
            List<int> listBakimRiski, int kullaniciID);

        int AddWithTransaction(PeriyodikBakim periyodikBakim, List<int> listBakimPlani, 
            List<int> listBakimRiski, int kullaniciID);

        List<PeriyodikBakimDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        List<PeriyodikBakim> GetListByVarlikID(int VarlikID);

        int DeleteSoftWithTransaction(int Id);
    }
}