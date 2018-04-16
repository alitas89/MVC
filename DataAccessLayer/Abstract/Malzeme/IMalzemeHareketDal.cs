using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Malzeme
{
    public interface IMalzemeHareketDal : IEntityRepository<MalzemeHareket>
    {
        List<MalzemeHareketDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        int AddWithTransaction(MalzemeHareketTemp malzemeHareketTemp, List<MalzemeHareketDetay> listMalzeme);

        int UpdateWithTransaction(MalzemeHareketTemp malzemeHareketTemp, List<MalzemeHareketDetay> listMalzeme);
    }
}
