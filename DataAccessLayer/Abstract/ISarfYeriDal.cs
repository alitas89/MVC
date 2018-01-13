using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    namespace DataAccessLayer.Abstract
    {
        public interface ISarfYeriDal : IEntityRepository<SarfYeri>
        {
            List<SarfYeriDto> GetListDto();
        }
    }
}