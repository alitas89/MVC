using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.Concrete.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;

namespace DataAccessLayer.Abstract.Varlik
{
    namespace DataAccessLayer.Abstract
    {
        public interface ISarfYeriDal : IEntityRepository<SarfYeri>
        {
            List<SarfYeriDto> GetListDto();

            List<SarfYeriDto> GetListPaginationDto(PagingParams pagingParams);

            List<SarfYeri> GetList(int IsletmeID);

            bool IsKodDefined(string Kod);
        }
    }
}