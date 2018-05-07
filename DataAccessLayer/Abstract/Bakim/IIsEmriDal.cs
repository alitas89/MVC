using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IIsEmriDal : IEntityRepository<IsEmri>
    {
        List<IsEmriDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        List<IsTipiForKullaniciTemp> GetIsTipiListByKullaniciID(int KullaniciID);
    }
}