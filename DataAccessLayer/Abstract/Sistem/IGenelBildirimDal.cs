using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Sistem;
using EntityLayer.Concrete.Sistem;

namespace DataAccessLayer.Abstract.Sistem
{
    public interface IGenelBildirimDal
    {
        int GetAcikOnaysizIsTalepSayisi(int KullaniciID);

        int GetAcikIsEmriSayisi(int KullaniciID);

        int GetSorumluOlunanIsEmriSayisi(int KullaniciID);

        List<IsEmriBakimSonucBildirimTemp> GetIsEmriBakimSonucBildirim(int KullaniciID);

        List<IsTalepSonucBildirimTemp> GetIsTalepSonucBildirim(int KullaniciID);
    }
}