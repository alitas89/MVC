using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace BusinessLayer.Abstract.Sistem
{
    public interface IGenelBildirimService
    {
        List<GenelBildirim> GetList();

        GenelBildirim GetById(int id);

        int Add(GenelBildirim genelbildirim);

        int Update(GenelBildirim genelbildirim);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<GenelBildirim> GetListPagination(PagingParams pagingParams);

        List<GenelBildirim> GetListPaginationByKime(PagingParams pagingParams, int KullaniciID);

        int GetCount(string filter = "");

        int GetCountByKime(int KullaniciID, string filter = "");

        List<GenelBildirim> GetListYeniBildirimByKime(int Kime);

        List<GenelBildirim> GetListByKime(int Kime);

        List<GenelBildirimKullaniciDto> GetListGenelBildirimKullaniciDtoByKime(int BildirimID);

        List<GenelBildirimYoneticiDto> GetListGenelBildirimYoneticiDtoByKime(int BildirimID);

        int UpdatePushOkundu(GenelBildirimPushOkundu genelBildirimPushOkundu);
    }
}