using System;
using Core.EntityLayer;

namespace EntityLayer.Concrete.Sistem
{
    // DB Model Class
    public class BildirimTrigger : IEntity
    {
        public int BildirimTriggerID { get; set; }
        public int PeriyotBirimID { get; set; }
        public int PeriyotDeger { get; set; }
        public int OlusturanID { get; set; }
        public int KimeID { get; set; }
        public int KimeTipID { get; set; }
        public string Ad { get; set; }
        public string Icerik { get; set; }
        public DateTime BaslangicTarih { get; set; }
        public DateTime? BitisTarih { get; set; }
        public int BildirimAksiyonSayfaID { get; set; }
        public int BildirimAksiyonID { get; set; }
        public DateTime? OlusturulmaTarih { get; set; }
        public DateTime? SonDegisiklikTarih { get; set; }
        public DateTime? QuartzTriggerTarih { get; set; }
        public bool IsTrigger { get; set; }
        public int QuartzJobTip { get; set; }
        public bool Silindi { get; set; }
    }
}