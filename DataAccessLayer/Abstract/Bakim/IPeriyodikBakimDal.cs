using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IPeriyodikBakimDal : IEntityRepository<PeriyodikBakim>
    {
        int UpdateWithTransaction(PeriyodikBakim periyodikBakim, List<int> listBakimPlani, List<int> listBakimRiski);

        int AddWithTransaction(PeriyodikBakim periyodikBakim, List<int> listBakimPlani, List<int> listBakimRiski);

        List<PeriyodikBakimDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");
    }
}