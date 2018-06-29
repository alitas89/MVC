using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace DataAccessLayer.Abstract.Sistem
{
    public interface IGenelBildirimDal : IEntityRepository<GenelBildirim>
    {
        List<GenelBildirim> GetListYeniBildirimByKime(int Kime);

        List<GenelBildirim> GetListByKime(int Kime);

        List<GenelBildirimKullaniciDto> GetListGenelBildirimKullaniciDtoByKime(int BildirimID, int KullaniciID);

        List<GenelBildirimYoneticiDto> GetListGenelBildirimYoneticiDtoByKime(int BildirimID, int KullaniciID);

        int UpdatePushOkundu(GenelBildirimPushOkundu genelBildirimPushOkundu);

        int GetCountByKime(int KullaniciID, string filter = "");

        List<GenelBildirim> GetListPaginationByKime(PagingParams pagingParams, int KullaniciID);
    }
}