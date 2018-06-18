using System;
using Core.EntityLayer;

namespace EntityLayer.Concrete.Sistem
{
    public class GenelBildirim : IEntity
    {
        public int BildirimID { get; set; }
        public int BildirimTriggerID { get; set; }
        public int Tip { get; set; }
        public int Kime { get; set; }
        public int KimeTip { get; set; }
        public string Ad { get; set; }
        public string Icerik { get; set; }
        public int BildirimAksiyonSayfaID { get; set; }
        public int BildirimAksiyonID { get; set; }
        public DateTime BildirimTarih { get; set; }
        public DateTime Tarih { get; set; }
        public DateTime? OkunmaTarih { get; set; }
        public DateTime? PushTarih { get; set; }
        public bool IsOkundu { get; set; }
        public bool IsPush { get; set; }
        public bool Silindi { get; set; }
    }

}