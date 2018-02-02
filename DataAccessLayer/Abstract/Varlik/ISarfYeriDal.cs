using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Abstract.Varlik
{
    namespace DataAccessLayer.Abstract
    {
        public interface ISarfYeriDal : IEntityRepository<SarfYeri>
        {
            List<SarfYeriDto> GetListDto();
        }
    }
}