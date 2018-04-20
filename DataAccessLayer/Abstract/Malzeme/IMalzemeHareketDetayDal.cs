using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Malzeme;
using EntityLayer.Concrete.Malzeme;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Malzeme
{
    public interface IMalzemeHareketDetayDal : IEntityRepository<MalzemeHareketDetay>
    {
        List<MalzemeHareketDetayDto> GetListByFisNo(int MalzemeHareketFisNo);
    }
}
