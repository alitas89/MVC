namespace BusinessLayer.Abstract.Sistem
{
    public interface IGenelBildirimService
    {
        int GetAcikOnaysizIsTalepSayisi(int KullaniciID);

        int GetAcikIsEmriSayisi(int KullaniciID);

        int GetSorumluOlunanIsEmriSayisi(int KullaniciID);
    }
}