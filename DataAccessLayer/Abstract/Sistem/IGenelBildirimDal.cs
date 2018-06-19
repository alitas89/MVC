using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Sistem;
using EntityLayer.Concrete.Sistem;

namespace DataAccessLayer.Abstract.Sistem
{
    public interface IGenelBildirimDal : IEntityRepository<GenelBildirim>
    {
        List<GenelBildirim> GetListYeniBildirimByKime(int Kime);

        List<GenelBildirim> GetListByKime(int Kime);

        List<GenelBildirimKullaniciDto> GetListGenelBildirimKullaniciDtoByKime(int BildirimID);

        List<GenelBildirimYoneticiDto> GetListGenelBildirimYoneticiDtoByKime(int BildirimID);
    }
}