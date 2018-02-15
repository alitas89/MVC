using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Abstract.Varlik
{
    public interface IKisimDal : IEntityRepository<Kisim>
    {
        List<KisimDto> GetListDto();

        List<Kisim> GetList(int SarfYeriID);
    }
}