using System;
using EntityLayer.Concrete.Sistem;

namespace EntityLayer.ComplexTypes.DtoModel.Sistem
{
    public class GenelBildirimYoneticiDto:GenelBildirim
    {
        public int PeriyotBirimID { get; set; }
        public int PeriyotDeger { get; set; }
        public int OlusturanID { get; set; }
        public int KimeTipID { get; set; }
        public DateTime BaslangicTarih { get; set; }
        public DateTime BitisTarih { get; set; }
        public DateTime OlusturulmaTarih { get; set; }
        public DateTime SonDegisiklikTarih { get; set; }
        public DateTime QuartzTriggerTarih { get; set; }
        public bool IsTrigger { get; set; }
        public string BirimAd { get; set; }
        public string KullaniciAdi { get; set; }
    }
}