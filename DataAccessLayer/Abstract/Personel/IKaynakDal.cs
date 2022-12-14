using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Personel;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace DataAccessLayer.Abstract.Personel
{
    public interface IKaynakDal : IEntityRepository<Kaynak>
    {

        List<KaynakDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        List<Kaynak> GetListKaynakHaveKullaniciID();

        List<string> AddListWithTransactionBySablon(List<Kaynak> listKaynak);
    }
}
