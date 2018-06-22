using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IPeriyodikBakimDal : IEntityRepository<PeriyodikBakim>
    {
        int UpdateWithTransaction(PeriyodikBakim periyodikBakim, List<int> listBakimPlani, List<int> listBakimRiski, int kullaniciID);

        int AddWithTransaction(PeriyodikBakim periyodikBakim, List<int> listBakimPlani, List<int> listBakimRiski, int kullaniciID);

        List<PeriyodikBakimDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        List<PeriyodikBakim> GetListByVarlikID(int VarlikID);

        List<IsTipiForKullaniciTemp> GetIsTipiListByKullaniciIDForIsEmri(int KullaniciID);
    }
}