using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Sistem;

namespace BusinessLayer.Abstract.Sistem
{
    public interface IGenelBildirimService
    {
        int GetAcikOnaysizIsTalepSayisi(int KullaniciID);

        int GetAcikIsEmriSayisi(int KullaniciID);

        int GetSorumluOlunanIsEmriSayisi(int KullaniciID);

        List<IsEmriBakimSonucBildirimTemp> GetIsEmriBakimSonucBildirim(int KullaniciID);

        List<IsTalepSonucBildirimTemp> GetIsTalepSonucBildirim(int KullaniciID);
    }
}